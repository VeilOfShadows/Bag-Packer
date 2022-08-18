using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    private Camera cam;
    public Transform currentObject;
    public Transform currentButton;
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

            if(Input.GetMouseButtonDown(0))
            {
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out RaycastHit hit2, 100f, layerMask))
                {
                    if(hit2.transform.CompareTag("Node"))
                    { 
                        currentObject.GetComponent<BagObject>().StartCoroutine("Place");
                        currentObject = null;


                        currentButton.gameObject.SetActive(false);
                        currentButton = null;
                    }
                }
            }
        }
    }

    public void PlaceButton()
    {
        currentObject.GetComponent<BagObject>().StartCoroutine("Place");
        currentObject = null;

        currentButton.gameObject.SetActive(false);
        currentButton = null;
    }

    public void RotateButton()
    {
        currentObject.transform.Rotate(new Vector3(0,90,0));
    }
}
