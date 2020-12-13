using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "char#")]
public class CharacterData : ScriptableObject
{
    public int id;

    public string characterName;

    public string pseudonym;

    public int overLordId;

    public KingdomData land;

    public List<int> subjectsIds;
    public List<string> possiblePseudonyms;

    public int strength;
    public int inteligence;
    public int charisma;

    public CharacterData(int i) 
    {
        id = i;
    }
}

