using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warmonger : Attribute
{
    public Warmonger()
    {
        strengthModifier = 1;
        inteligenceModifier = 3;
        charismaModifier = -2;
        availablePseudonyms = new List<string>() { "Peace-Breaker", "Hammer of war", "The Conqueror" };
    }
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public override void Apply(Character iChar)
    {
        iChar.Data.charisma += charismaModifier;
        iChar.Data.strength += strengthModifier;
        iChar.Data.inteligence += inteligenceModifier;
        iChar.Data.possiblePseudonyms.AddRange(availablePseudonyms);
    }

    public override void Remove(Character iChar)
    {
        iChar.Data.charisma -= charismaModifier;
        iChar.Data.strength -= strengthModifier;
        iChar.Data.inteligence -= inteligenceModifier;
        foreach (var item in availablePseudonyms)
        {
            iChar.Data.possiblePseudonyms.Remove(item);
        }
    }
}
