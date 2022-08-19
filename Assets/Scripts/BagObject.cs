using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagObject : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<GameObject> nodes = new List<GameObject>();

    [SerializeField]
    private List<BoxCollider> colliders;

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private LayerMask layerMask;
    public bool isValid = true;

    bool coroutineStarted = false;
    bool placing = false;
    bool canBePlaced = true;
    bool detectTriggers = true;

    public Color indicatorNodeColour;
    public Color nodeColour;
    public Color errorNodeColour;
    public GameManager gameManager;

    RaycastHit hit;
    #endregion

    #region Collider Triggers
    private void OnTriggerEnter(Collider other)
    {
        if(detectTriggers)
        {
            if(!other.CompareTag("Bounds"))
            {
                gameManager.canBePlaced = true;
                isValid = true;
            }

            if(other.CompareTag("Node"))
            {
                if(isValid)
                {
                    other.GetComponentInChildren<MeshRenderer>().material.color = indicatorNodeColour;
                }
                else
                {
                    other.GetComponentInChildren<MeshRenderer>().material.color = errorNodeColour;

                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(detectTriggers)
        {
            if(other.CompareTag("Node"))
            {
                other.GetComponentInChildren<MeshRenderer>().material.color = nodeColour;
            }

            if(other.CompareTag("Bounds"))
            {
                gameManager.canBePlaced = true;
                isValid = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(detectTriggers)
        {
            if(!placing)
            {
                if(other.CompareTag("Bounds"))
                {
                    gameManager.canBePlaced = false;
                    isValid = false;
                }
                else if(other.CompareTag("Node"))
                {
                    if(isValid)
                    {
                        other.GetComponentInChildren<MeshRenderer>().material.color = indicatorNodeColour;
                    }
                    else
                    {
                        other.GetComponentInChildren<MeshRenderer>().material.color = errorNodeColour;

                    }
                }


                return;
            }
            else
            {
                if(other.CompareTag("Node"))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
    #endregion

    #region Placement Coroutine
    public IEnumerator Place()
    {
        if(coroutineStarted)
        {
            yield return null;
        }
        coroutineStarted = true;

        anim.Play();

        placing = true;
        yield return new WaitForSeconds(0.1f);
        detectTriggers = false;

        //for(int i = 0; i < colliders.Count; i++)
        //{
        //    colliders[i].enabled = false;
        //}

        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i].SetActive(true);
            nodes[i].transform.parent = this.transform.parent;
        }

        coroutineStarted = false;
        yield return null;
    }
    #endregion
}


