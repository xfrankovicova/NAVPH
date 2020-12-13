using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coward : Attribute
{
    public override bool CanApply(Character iChar)
    {
        return true;
    }
    public Coward()
    {
        strengthModifier = -3;
        inteligenceModifier = -1;
        charismaModifier = -3;
        availablePseudonyms = new List<string>() { "Affraid", "Turd-fighter" };
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
