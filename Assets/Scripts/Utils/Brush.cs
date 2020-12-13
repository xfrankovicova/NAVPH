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

    [SerializeField]
    private DemoScene uiListener;

    private void Start()
    {
     //   uiListener = GameObject.Find("Canvas").GetComponent<DemoScene>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2) && !uiListener.isUIOverride)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                var hexagon = objectHit.gameObject.GetComponent<Hexagon>();
                if (hexagon == null)
                {
                    Debug.Log(objectHit.name);
                }
                else
                {
                    var clickedKingdom = KingdomController.Instance.getKingdom(hexagon.CurrentKingdomId);
                    Debug.Log("Clicked on " + clickedKingdom.FullName + " #" + clickedKingdom.data.id);
                }

                // Do something with the object that was hit by the raycast.
            }
        }

    }

}
