using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    private static Brush _instance;
    public static Brush Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private Hextypes currentHexType;
    public Hextypes CurrentHexType { get => currentHexType; set => currentHexType = value; }
    private bool bushHextypes = true;

    [SerializeField]
    private int currentkingdomId;
    public int CurrentKingdomId { get => currentkingdomId; set => currentkingdomId = value; }
    public bool BushHextypes { get => bushHextypes && !uiListener.isUIOverride; set => bushHextypes = value; }
    public bool BushKingdoms { get => bushKingdoms && !uiListener.isUIOverride; set => bushKingdoms = value; }

    private bool bushKingdoms = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Debug.Log(_instance.ToString() + " created");
    }

    private DemoScene uiListener;

    private void Start()
    {
        uiListener = GameObject.Find("Canvas").GetComponent<DemoScene>();
    }



}
