using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kingdom
{
    [SerializeField]
    public KingdomData data;
    [SerializeField]
    private int kingdomId;
    public int KingdomId => kingdomId;
    public int KingdomSize => data.hexes.Count;
    public Kingdom(int iKingdomId)
    {
        kingdomId = iKingdomId;
        data = Resources.Load<KingdomData>("Kingdoms/kingdom_" + kingdomId.ToString());
            data.hexes = new List<Hexagon>();

        Debug.Log("Kingdom #" + kingdomId + " hexes ");

        foreach (var item in data.hexes)
        {
            Debug.Log("Kingdom #" + kingdomId + ". " + item.name);
        }
        data.borderMat = Resources.Load<Material>("Materials/mat" + kingdomId.ToString());
    }

    public void AddHex(Hexagon hex)
    {
        if (!data.hexes.Contains(hex))
        {
            data.hexes.Add(hex);
        }
    }

    public void UpdateBorder()
    {
        foreach (var hex in data.hexes)
        {
            hex.SetBorderMat(data.borderMat);
        }
    }
}

[CreateAssetMenu]
public class KingdomData : ScriptableObject 
{
    public int id;

    public List<Hexagon> hexes;

    public Material borderMat;

    public int overLordId;

    public List<int> subjectsIds;
}
