using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignorant : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Ignorant()
    {
        strengthModifier = -1;
        inteligenceModifier = 3;
        charismaModifier = -1;
        availablePseudonyms = new List<string>() { "Not-seeing", "Selfish", "Unloved" };
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
