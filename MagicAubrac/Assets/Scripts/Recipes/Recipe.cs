using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipes/Create recipe")]
public class Recipe : ScriptableObject
{
    [Description("Used for debug")]
    public string Name;
    public Rune ActivationRune;
    public Step[] Steps;

    public string GetDebugString()
    {
        return Name;
    }
}
