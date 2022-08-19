using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectManager : MonoBehaviour
{
    #region Variables
    public Transform iconHolder;
    public Transform currentBag;
    public TextMeshProUGUI itemsLeftText;
    public List<GameObject> possibleObjects = new List<GameObject>();
    public List<GameObject> objectsToPlace = new List<GameObject>();
    public int amount;
    public bool randomise;
    public int itemsLeftCount = 0;
    private LevelManager levelManager;
    #endregion

    #region Unity Methods
    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if(randomise)
        {
            for(int i = 0; i < amount; i++)
            {
                objectsToPlace.Add(possibleObjects[Random.Range(0, possibleObjects.Count)]);
            }


            for(int i = 0; i < objectsToPlace.Count; i++)
            {
                GameObject  go = Instantiate(objectsToPlace[i], iconHolder);
                go.GetComponent<ObjectButton>().objectManager = this;
            }
        }
        UpdateItemsLeft();
    }
    #endregion

    #region UI updates
    public void UpdateItemsLeft()
    {
        itemsLeftCount = 0;
        foreach(Transform child in iconHolder)
        {
            if(child.gameObject.active)
            {
                itemsLeftCount += 1;
            }
        }
        itemsLeftText.SetText(itemsLeftCount.ToString());

        if(itemsLeftCount <= 0)
        {
            Debug.Log("LEVEL DONE");
            levelManager.CompleteLevel();
        }
    }
    #endregion
}
