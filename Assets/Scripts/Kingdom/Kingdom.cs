using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingdom
{
    [SerializeField]
    private int kingdomId;
    public int KingdomId => kingdomId;

    [SerializeField]
    private List<Hexagon> hexes;

    [SerializeField]
    private Material borderMat;

    public int KingdomSize => hexes.Count;

    public Kingdom(int iKingdomId)
    {
        kingdomId = iKingdomId;
        hexes = new List<Hexagon>();
        borderMat = Resources.Load<Material>("Materials/mat" + kingdomId.ToString());
        Debug.Log(borderMat);
    }

    public void AddHex(Hexagon hex)
    {
        if (!hexes.Contains(hex))
        {
            hexes.Add(hex);
        }
    }

    public void UpdateBorder()
    {
        foreach (var hex in hexes)
        {
            hex.SetBorderMat(borderMat);
        }
    }
}
