using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crippled : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Crippled()
    {
        strengthModifier = -7;
        inteligenceModifier = 0;
        charismaModifier = -1;
        availablePseudonyms = new List<string>() { "The Cripple", "Stone-hand", "Stone-leg", "Broken" };
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
