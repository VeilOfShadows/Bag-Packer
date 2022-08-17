using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagObject : MonoBehaviour
{
    public List<GameObject> nodes = new List<GameObject>();
    public BoxCollider collider;
    RaycastHit hit;
    public LayerMask layerMask;
    bool coroutineStarted = false;

    //public void Place()
    //{
    //    collider.enabled = true;
    //    for(int i = 0; i < nodes.Count; i++)
    //    {
    //        nodes[i].SetActive(true);
    //    }
    //}

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Node"))
        {
        Debug.Log("HIT");
            Destroy(other.gameObject);
        }
    }

    public IEnumerator Place()
    {
        if(coroutineStarted)
        {
            yield return null;
        }
        coroutineStarted = true;
        
        collider.enabled = true;
        yield return new WaitForSeconds(0.1f);        
        collider.enabled = false;
        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i].SetActive(true);
            nodes[i].transform.parent = this.transform.parent;
        }
        coroutineStarted = false;
        yield return null;
    }

}


