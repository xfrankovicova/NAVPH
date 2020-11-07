using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int gridSizeX = 40;
    public const int gridSizeY = 25;

    public static Dictionary<SoldiersType, SoldierStats> soldierDictionary = new Dictionary<SoldiersType, SoldierStats>() {
        { SoldiersType.Pesant, new SoldierStats(20, 0, 10, 10, 20, 5, 60, 0, 1, 8, 25) },
        { SoldiersType.Spearman, new SoldierStats(20, 2, 40, 25, 40, 8, 75, 0, 1, 6, 30) }
        //{ SoldiersType., new SoldierStats() }
    };
}

public enum Hextypes
{
    //None, Forrest, Field, Town, Castle, Mountains
    Water, Grassland, City, Castle, Village, Mountain_Big, Mountain_Small, Forrest, Mine, Field
}

public enum SoldiersType
{
    Pesant, Spearman, Zwaihander, Men_At_Arms, Bowman, Crossbowman, Knight
}

public class SoldierStats 
{
    public int health;
    public int armor;
    public int meleDefence;
    public int rangeDefence;
    public int cavaleryDefenceBonus;
    public int damage;
    public int meleAtack;
    public int rangeAtact;
    public int charge;
    public int speed;
    public int courage;

    public SoldierStats(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11)
    {
        this.health = v1;
        this.armor = v2;
        this.meleDefence = v3;
        this.rangeDefence = v4;
        this.cavaleryDefenceBonus = v5;
        this.damage = v6;
        this.meleAtack = v7;
        this.rangeAtact = v8;
        this.charge = v9;
        this.speed = v10;
        this.courage = v11;
    }
}