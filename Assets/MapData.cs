using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public Hextypes[,] mapTypes = new Hextypes[Constants.gridSizeX, Constants.gridSizeY];
    public int[,] mapKingdoms = new int[Constants.gridSizeX, Constants.gridSizeY];
}
