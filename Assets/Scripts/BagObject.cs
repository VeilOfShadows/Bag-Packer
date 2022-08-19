using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagObject : MonoBehaviour
{
    #region Variables
    public List<GameObject> nodes = new List<GameObject>();
    public List<BoxCollider> colliders;
    public Animation anim;
    RaycastHit hit;
    public LayerMask layerMask;
    bool coroutineStarted = false;
    bool placing = false;

    public Color nodeColour;
    public Color indicatorNodeColour;
    #endregion

    #region Collider Triggers
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Node"))
        {
            //Debug.Log("HIT");
            //if(placing)
            //{
            //    Destroy(other.gameObject);
            //}
            other.GetComponentInChildren<MeshRenderer>().material.color = indicatorNodeColour;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Node"))
        {
            //Debug.Log("HIT");
            //if(placing)
            //{
            //    Destroy(other.gameObject);
            //}
            other.GetComponentInChildren<MeshRenderer>().material.color = nodeColour;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(!placing)
        {
            return;
        }
        else
        {
            if(other.CompareTag("Node"))
            {
                Debug.Log("HIT");
                Destroy(other.gameObject);
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
        //for(int i = 0; i < colliders.Count; i++)
        //{
        //    colliders[i].enabled = true;
        //}

        placing = true;
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }
        yield return new WaitForSeconds(0.1f);
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


