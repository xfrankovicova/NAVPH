using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Soldier
{
    // Start is called before the first frame update
    void Start()
    {
        stats = Constants.soldierDictionary[SoldiersType.Spearman];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
