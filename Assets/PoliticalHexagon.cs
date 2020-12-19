using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PoliticalHexagon : MonoBehaviour
{
    public void setMat(Material mat) 
    {
        Material[] materials = GetComponent<MeshRenderer>().materials;
        materials[1] = mat;
        GetComponent<MeshRenderer>().materials = materials;
    }
}
