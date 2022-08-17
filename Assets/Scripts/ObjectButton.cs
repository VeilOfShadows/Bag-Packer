using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public PlacementController placementController;
    public ObjectManager objectManager;
    public GameObject objectToInstantiate;
    public bool selected = false;

    private void Start()
    {
        placementController = FindObjectOfType<PlacementController>();
    }

    public void InstantiateObject()
    {
        if(selected)
        {
            return;
        }
        selected = true;
        placementController.currentObject = Instantiate(objectToInstantiate, objectManager.currentBag).transform;
        placementController.currentButton = this.transform;
    }
}
