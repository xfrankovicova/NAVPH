using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexTypeController : MonoBehaviour
{

    private static HexTypeController instance;
    public static HexTypeController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        Debug.Log("Awake");
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
    }


    [SerializeField]
    private Sprite[] hexSprites;
    // Start is called before the first frame update

    public Sprite GetHex(Hextypes type) 
    {
        Debug.Log(type.ToString());
        return hexSprites[(int)type];
    }



}

