using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliticalHexagon : MonoBehaviour
{
    private Material mat;

    public void setMat() 
    {
        GetComponent<MeshRenderer>().materials[1] = mat;
    }
}
