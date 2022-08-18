using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public MeshRenderer renderer;
    public void MaterialSwap()
    {
        if(renderer.material == mat1)
        {
            renderer.material = mat2;
        }
        else
        {
            renderer.material = mat1;

        }
    }
}
