using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    #region Variables
    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject objectToInstantiate;
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
        gameManager.currentObject.GetComponent<BagObject>().indicatorNodeColour = gameManager.indicatorNodeColour;
        gameManager.currentButton = this.transform;
    }
    #endregion
}
