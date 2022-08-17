using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    //Node[,,] grid;
    public int xSize;
    public int ySize;
    public int zSize;

    public Vector3 extends;
    //public List<GameObject> bagsToInitialise = new List<GameObject>();
    public GameObject node;

    //int pos_x;
    //int pos_y;
    //int pos_z;

    private void Start()
    {
        CreateGrid(); 
    }

    public void CreateGrid()
    {
        //grid = new Node[xSize, ySize, zSize];

        for(int y = 0; y < ySize; y++)
        {
            for(int x = 0; x < xSize; x++)
            {
                for(int z = 0; z < zSize; z++)
                {
                    Vector3 offset = new Vector3(xSize, ySize, zSize);
                    GameObject n = Instantiate(node, transform);
                    n.transform.position += offset;
                    //n.transform.parent = this.gameObject.transform;
                    //n.pare
                    //n.transform
                    //Node n = new Node();
                    //n.x = x;
                    //n.y = y;
                    //n.z = z;

                    //n.worldPosition = new Vector3(x,y,z);
                    //Debug.Log(n.worldPosition);
                    //grid[x, y, z] = n;

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //foreach(Node node in grid)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireCube(node.worldPosition, extends);
        //}  
    }
}
