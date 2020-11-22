using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settlement : ScriptableObject
{
    [SerializeField]
    private string settlementName;

    [SerializeField]
    private int wealth;

    [SerializeField]
    private int freeman;

    [SerializeField]
    private Kingdom kingdom;

    [SerializeField]
    private List<Building> buildings;

    [SerializeField]
    private int availableBuildingSlots;

    public void Save() 
    {
    
    }
}

[CreateAssetMenu]
public abstract class Building : ScriptableObject
{

}

[CreateAssetMenu]
public class Port : Building 
{

}

[CreateAssetMenu]
public class Smith : Building
{

}