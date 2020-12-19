using UnityEngine;
using System.Collections.Generic;
using System;

public class BattleNode
{
    public List<BattleNode> neighbours;
    public int x;
    public int y;

    public BattleNode()
    {
        neighbours = new List<BattleNode>();
    }

    public float DistanceTo(BattleNode n)
    {
        if (n == null)
        {
            Debug.LogError("WTF?");
        }

        return Vector2.Distance(
            new Vector2(x, y),
            new Vector2(n.x, n.y)
            );
    }

    public List<Tuple<int, int>> GetNeighboursIndexes()
    {
        List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        //get left
        neighbours.Add(new Tuple<int, int>(x - 1, y));
        //get right 
        neighbours.Add(new Tuple<int, int>(x + 1, y));
        //get down 
        neighbours.Add(new Tuple<int, int>(x, y - 1));
        //get up 
        neighbours.Add(new Tuple<int, int>(x, y + 1));
        //if even
        if (x % 2 == 0)
        {
            neighbours.Add(new Tuple<int, int>(x - 1, y + 1));
            neighbours.Add(new Tuple<int, int>(x + 1, y + 1));
        }
        else
        {
            neighbours.Add(new Tuple<int, int>(x - 1, y - 1));
            neighbours.Add(new Tuple<int, int>(x + 1, y - 1));
        }

        foreach (var item in neighbours)
        {
            if (!(item.Item1 < 0 || item.Item1 >= Constants.battleMapSizeX || item.Item2 < 0 || item.Item2 >= Constants.battleMapSizeY))
            {
                result.Add(item);
            }
        }

        return result;
    }

}
