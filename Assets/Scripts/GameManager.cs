using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    private Camera cam;
    public ObjectManager objectManager;

    [SerializeField]
    private Transform mask;

    [SerializeField]
    private Material nodeMat;

    [SerializeField]
    private Color transparentNodeColour;

    [SerializeField]
    AudioSource audio;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float snapSize;

    private Vector3 worldPosition;
    private Vector3 pos;
    private Animation anim;

    public Transform currentObject;
    public Transform currentButton;

    public Color nodeColour;
    public Color indicatorNodeColour;
    public Color errorNodeColour;

    public Transform blocker;

    public bool isTutorial;
    public TutorialManager tutorialManager;    

    public bool canBePlaced;
    #endregion

    #region Unity Methods
    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(currentObject != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                worldPosition = hit.transform.position;

                pos.x = worldPosition.x + 1.5f;
                pos.y = worldPosition.y;
                pos.z = worldPosition.z + 1f;

                if(hit.transform.root == objectManager.currentBag)
                {
                    currentObject.localPosition = pos;
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(objectManager.currentBag == null)
                {
                    Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(mouseRay, out RaycastHit mouseHit, 300f))
                    {
                        if(mouseHit.transform.CompareTag("Bag")) 
                        {
                            OpenBag(mouseHit.transform);
                            if(isTutorial && tutorialManager.tutorialStep == 0)
                            {
                                Destroy(tutorialManager.openBagPrompt);
                                tutorialManager.selectItemPrompt.SetActive(true);
                                tutorialManager.tutorialStep = 1;
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Buttons
    public void Place()
    {
        //Handheld.Vibrate();
        if(!canBePlaced)
        {
            return;
        }

        currentObject.GetComponent<BagObject>().StartCoroutine("Place");
        currentObject = null;

        anim = objectManager.currentBag.GetComponent<Animation>();
        anim.Play("BagBounce");
        anim = null;
        objectManager.currentBag.GetComponent<AudioSource>().Play();

        //Destroy(currentButton);
        currentButton.gameObject.SetActive(false);
        currentButton = null;
        objectManager.UpdateItemsLeft();
        if(isTutorial && tutorialManager.tutorialStep == 3)
        {
            Destroy(tutorialManager.confirmPrompt);
            tutorialManager.closeBagPrompt.SetActive(true);
            tutorialManager.tutorialStep = 4;
        }
    }

    public void RotateButton()
    {
        currentObject.transform.Rotate(new Vector3(0,90,0));
        if(isTutorial && tutorialManager.tutorialStep == 2)
        {
            Destroy(tutorialManager.rotatePrompt);
            tutorialManager.confirmPrompt.SetActive(true);
            tutorialManager.tutorialStep = 3;
        }
    }
    #endregion

    #region Bag Controls
    public void OpenBag(Transform bag)
    {
        blocker.gameObject.SetActive(false);
        audio.Play();
        anim = bag.GetComponent<Animation>();
        anim[bag.name].speed = 1;
        anim.Play(bag.name);
        mask.GetComponent<Animator>().SetTrigger("FadeIn");
        objectManager.currentBag = bag;
        anim = null;
        nodeMat.color = nodeColour;
    }

    public void CloseBag()
    {
        if(currentButton)
        {
            Destroy(currentObject.gameObject);
            currentObject = null;
            currentButton.GetComponent<ObjectButton>().selected = false;
            currentButton = null;
        }

        blocker.gameObject.SetActive(true);
        audio.Play();
        Transform bag = objectManager.currentBag;
        anim = bag.GetComponent<Animation>();
        anim[bag.name].speed = -1;
        anim.Play(bag.name);
        mask.GetComponent<Animator>().SetTrigger("FadeOut");
        objectManager.currentBag = null;
        anim = null;
        nodeMat.color = transparentNodeColour;
        if(isTutorial && tutorialManager.tutorialStep == 4)
        {
            Destroy(tutorialManager.closeBagPrompt);
            isTutorial = false;
            tutorialManager.tutorialStep = 5;
        }
    }
    #endregion
}
