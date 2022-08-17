using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public Transform iconHolder;
    public Transform currentBag;
    public List<GameObject> possibleObjects = new List<GameObject>();
    public List<GameObject> objectsToPlace = new List<GameObject>();
    public int amount;

    public void Start()
    {
        for(int i = 0; i < amount; i++)
        {
            objectsToPlace.Add(possibleObjects[Random.Range(0, possibleObjects.Count)]);
        }

        //Vector3 startPos = new Vector3(-2, -5, -0.5f);

        for(int i = 0; i < objectsToPlace.Count; i++)
        {
            GameObject  go = Instantiate(objectsToPlace[i], iconHolder);
            go.GetComponent<ObjectButton>().objectManager = this;
            //go.transform.localPosition = new Vector3(startPos.x + (i + 1), startPos.y, startPos.z - (i + 1));
        }
    }
}
