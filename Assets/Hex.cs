using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hex : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Hextypes _hexType;

    private Hextypes currentHexType;

    public Hextypes HexType
    {
        get { return _hexType; }
    }    // Start is called before the first frame update
    void Start()
    {
        currentHexType = Hextypes.Water;
        sr.sprite = Resources.Load<Sprite>("Sprites/Hexes/" + currentHexType.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHexType != _hexType)
        {
            currentHexType = _hexType;
            sr.sprite = Resources.Load<Sprite>("Sprites/Hexes/" + currentHexType.ToString());
        }
    }
}
