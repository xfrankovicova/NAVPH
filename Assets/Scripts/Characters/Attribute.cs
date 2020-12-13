using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attribute : MonoBehaviour
{
    [SerializeField] internal Sprite img;
    [SerializeField] internal int strengthModifier;
    [SerializeField] internal int inteligenceModifier;
    [SerializeField] internal int charismaModifier;
    [SerializeField] internal List<string> availablePseudonyms;

    public abstract bool CanApply(Character iChar);
    public abstract void Apply(Character iChar);
    public abstract void Remove(Character iChar);
}
