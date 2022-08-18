using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject objectToInstantiate;
    public bool selected = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

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
        gameManager.currentButton = this.transform;
    }
}
