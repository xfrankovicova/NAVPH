using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beloved : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Beloved()
    {
        strengthModifier = 0;
        inteligenceModifier = 0;
        charismaModifier = 2;
        availablePseudonyms = new List<string>() { "Beloved", "Champion of common people" };
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
