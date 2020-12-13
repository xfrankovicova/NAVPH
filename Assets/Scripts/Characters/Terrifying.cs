using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrifing : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Terrifing()
    {
        strengthModifier = 2;
        inteligenceModifier = 2;
        charismaModifier = -1;
        availablePseudonyms = new List<string>() { "Iron-hearted", "Unsmiling" };
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
