using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    public ObjectManager objectManager;
    public Transform mask;
    public Transform currentObject;
    public Transform currentButton;
    public Vector3 worldPosition;
    public Vector3 pos;
    public Material nodeMat;
    public AudioSource audio;

    public Animation anim;

    public LayerMask layerMask;
    public float snapSize;

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

                currentObject.localPosition = pos;
            }

            if(Input.GetMouseButtonDown(0))
            {
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray2, out RaycastHit hit2, 100f, layerMask))
                {
                    if(hit2.transform.CompareTag("Node"))
                    { 
                        currentObject.GetComponent<BagObject>().StartCoroutine("Place");
                        currentObject = null;

                        anim = objectManager.currentBag.GetComponent<Animation>();
                        anim.Play("BagBounce");
                        anim = null;
                        objectManager.currentBag.GetComponent<AudioSource>().Play();

                        currentButton.gameObject.SetActive(false);
                        currentButton = null;
                    }
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(objectManager.currentBag == null)
                {
                    Debug.Log("RAY");
                    Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(mouseRay, out RaycastHit mouseHit, 300f))
                    {
                        if(mouseHit.transform.CompareTag("Bag")) 
                        {
                            OpenBag(mouseHit.transform);
                        }
                    }
                }
            }
        }
    }

    public void PlaceButton()
    {
        currentObject.GetComponent<BagObject>().StartCoroutine("Place");
        currentObject = null;

        currentButton.gameObject.SetActive(false);
        currentButton = null;
    }

    public void RotateButton()
    {
        currentObject.transform.Rotate(new Vector3(0,90,0));
    }

    public void OpenBag(Transform bag)
    {
        audio.Play();
        anim = bag.GetComponent<Animation>();
        anim[bag.name].speed = 1;
        anim.Play(bag.name);
        mask.GetComponent<Animator>().SetTrigger("FadeIn");
        objectManager.currentBag = bag;
        anim = null;
        nodeMat.color = new Color(nodeMat.color.r, nodeMat.color.g, nodeMat.color.b, 1f);
        //nodeMat.EnableKeyword("_EMISSION");
    }

    public void CloseBag()
    {
        audio.Play();
        Transform bag = objectManager.currentBag;
        anim = bag.GetComponent<Animation>();
        anim[bag.name].speed = -1;
        anim.Play(bag.name);
        mask.GetComponent<Animator>().SetTrigger("FadeOut");
        objectManager.currentBag = null;
        anim = null;
        nodeMat.color = new Color(nodeMat.color.r, nodeMat.color.g, nodeMat.color.b, 0f);
        //nodeMat.DisableKeyword("_EMISSION");
    }
}
