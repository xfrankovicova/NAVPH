using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrassland : Hex
{
    // Start is called before the first frame update
    void Start()
    {
        movementCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int iKingdomId, int Xcord, int Ycord)
    {
        x = Xcord;
        y = Ycord;
        currentKingdomId = iKingdomId;
    }
}
