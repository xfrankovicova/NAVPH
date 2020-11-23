using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KingdomData : ScriptableObject 
{
    public string kingdomName;

    public int id;

    public List<Hexagon> hexes;

    public Material borderMat;

    public int overLordId;

    public List<int> subjectsIds;
}
