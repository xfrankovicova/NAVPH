using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Hex
{
    public Hextypes currentHexType;

    public int x;

    public int y;

    [SerializeField]
    protected int movementCost;

    [SerializeField]
    protected int currentKingdomId;

    public bool isWalkable = true;

    public Hextypes HexType
    {
        get { return currentHexType; }
    }    

    void Start()
    {

    }

    void OnMouseUp()
    {
        Debug.Log("Click!");

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Generating!");
        //map.GeneratePathTo(x, y);
    }
}
