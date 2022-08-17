using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    private Camera cam;
    public Transform currentObject;
    public Vector3 worldPosition;
    public Vector3 pos;

    public LayerMask layerMask;
    public float snapSize;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(currentObject != null)
        {

            if(Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                worldPosition = hit.transform.position;

                pos.x = worldPosition.x + 1.5f;
                pos.y = worldPosition.y;
                pos.z = worldPosition.z + 1f;

                currentObject.localPosition = pos;
            }

            if(Input.GetKeyDown(KeyCode.J))
            {
                currentObject.GetComponent<BagObject>().StartCoroutine("Place");
                currentObject = null;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    hit.transform.GetComponent<BoxCollider>().enabled = false;
                    currentObject = hit.transform;
                }
            }
        }
    }
}
