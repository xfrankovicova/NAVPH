using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hated : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Hated()
    {
        strengthModifier = 1;
        inteligenceModifier = 0;
        charismaModifier = -5;
        availablePseudonyms = new List<string>() { "Iron-hearted", "Unsmiling", "Whore-son", "Unloved" };
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
