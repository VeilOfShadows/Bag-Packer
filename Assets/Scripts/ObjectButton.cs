using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject objectToInstantiate;

    public ObjectManager objectManager;

    public bool selected = false;
    #endregion

    #region Unity Methods
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objectManager = gameManager.objectManager;
    }
    #endregion

    #region Object Instantiation
    public void InstantiateObject()
    {
        if(gameManager.isTutorial && gameManager.tutorialManager.tutorialStep == 1)
        {
            Destroy(gameManager.tutorialManager.selectItemPrompt);
            gameManager.tutorialManager.rotatePrompt.SetActive(true);
            gameManager.tutorialManager.tutorialStep = 2;
        }

        if(selected)
        {
            return;
        }
        selected = true;

        objectManager.GetComponent<AudioSource>().Play();
        if(gameManager.currentButton)
        {
            Destroy(gameManager.currentObject.gameObject);
            gameManager.currentObject = null;
            gameManager.currentButton.GetComponent<ObjectButton>().selected = false;
            gameManager.currentButton = null;
        }

        gameManager.currentObject = Instantiate(objectToInstantiate, objectManager.currentBag.GetComponent<NodeHolder>().nodeHolder).transform;
        gameManager.currentObject.GetComponent<BagObject>().nodeColour = gameManager.nodeColour;
        gameManager.currentObject.GetComponent<BagObject>().errorNodeColour = gameManager.errorNodeColour;
        gameManager.currentObject.GetComponent<BagObject>().indicatorNodeColour = gameManager.indicatorNodeColour;
        gameManager.currentObject.GetComponent<BagObject>().gameManager = gameManager;
        gameManager.currentButton = this.transform;
    }
    #endregion
}
