using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heretic : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Heretic()
    {
        strengthModifier = 1;
        inteligenceModifier = 3;
        charismaModifier = -3;
        availablePseudonyms = new List<string>() { "Prophet of false gods", "Faith-breaker" };
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
