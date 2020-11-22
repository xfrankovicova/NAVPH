using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int gridSizeX = 40;
    public const int gridSizeY = 25; 
    public const int battleMapSizeX = 30;
    public const int battleMapSizeY = 20;

    public static Dictionary<SoldiersType, SoldierStats> soldierDictionary = new Dictionary<SoldiersType, SoldierStats>() {
        { SoldiersType.Pesant, new SoldierStats(20, 0, 10, 10, 20, 5, 60, 0, 1, 8, 25) },
        { SoldiersType.Spearman, new SoldierStats(20, 2, 40, 25, 40, 8, 75, 0, 1, 6, 30) }
        //{ SoldiersType., new SoldierStats() }
    };

    public static Dictionary<Hextypes, float> costToEnter = new Dictionary<Hextypes, float>() {
        { Hextypes.Water, Mathf.Infinity},
        { Hextypes.Grassland, 1f},
        { Hextypes.City, 2f},
        { Hextypes.Castle, 2f},
        { Hextypes.Village, 2f},
        { Hextypes.Mountain_Big, Mathf.Infinity},
        { Hextypes.Mountain_Small, Mathf.Infinity},
        { Hextypes.Forrest, 4f},
        { Hextypes.Mine, 7f},
        { Hextypes.Field, 3f}
    };

    public static Dictionary<BattleHextypes, float> costToEnterBattle = new Dictionary<BattleHextypes, float>() {
        { BattleHextypes.Water, Mathf.Infinity},
        { BattleHextypes.Grassland, 1f},
        { BattleHextypes.Mountain_Big, Mathf.Infinity},
        { BattleHextypes.Mountain_Small, Mathf.Infinity},
        { BattleHextypes.Forrest, 4f}
    };

    public static List<Tuple<int, int>> nodeNeighboursEven = new List<Tuple<int, int>>() {
        { new Tuple<int, int>( -1, 0)},
        { new Tuple<int, int>( 1, 0)},
        { new Tuple<int, int>( 0, -1)},
        { new Tuple<int, int>( 0, 1)},
        { new Tuple<int, int>( -1, 1)},
        { new Tuple<int, int>( 1, 1)}
    };

    public static List<Tuple<int, int>> nodeNeighboursOdd = new List<Tuple<int, int>>() {
        { new Tuple<int, int>( -1, 0)},
        { new Tuple<int, int>( 1, 0)},
        { new Tuple<int, int>( 0, -1)},
        { new Tuple<int, int>( 0, 1)},
        { new Tuple<int, int>( -1, 1)},
        { new Tuple<int, int>( 1, 1)}
    };


    public static Dictionary<Tuple<int, int>, int> neighboursEven = new Dictionary<Tuple<int, int>, int>() {
        { new Tuple<int, int>( -1, 0), 4},
        { new Tuple<int, int>( 1, 0), 2},
        { new Tuple<int, int>( 0, -1), 3},
        { new Tuple<int, int>( 0, 1), 0},
        { new Tuple<int, int>( -1, 1), 5},
        { new Tuple<int, int>( 1, 1), 1}
    };

    public static Dictionary<Tuple<int, int>, int> neighboursOdd = new Dictionary<Tuple<int, int>, int>() {
        { new Tuple<int, int>( -1, 0), 5},
        { new Tuple<int, int>( 1, 0), 1},
        { new Tuple<int, int>( 0, -1), 3},
        { new Tuple<int, int>( 0, 1), 0},
        { new Tuple<int, int>( -1, -1), 4},
        { new Tuple<int, int>( 1, -1), 2}
    };

}

public enum Hextypes
{
    Water, Grassland, City, Castle, Village, Mountain_Big, Mountain_Small, Forrest, Mine, Field
}

public enum BattleHextypes
{
    Water, Grassland, Mountain_Big, Mountain_Small, Forrest
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