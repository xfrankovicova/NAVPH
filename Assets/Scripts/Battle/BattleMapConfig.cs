using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleMapConfig : ScriptableObject
{
    public int hills;
    public int forests;
    public int lakes;
    public int startingProbability;
    public int decreasingProbability;
}
