using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPM : MonoBehaviour
{
    public Material mat;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        Material[] materials = mr.materials;

        materials[1] = mat;
        mr.materials = materials;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mr.materials[1].name.ToString());
        Debug.Log(mr.materials[0].name.ToString());
    }
}
