using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipes/Create recipe")]
public class Recipe : ScriptableObject
{
    public Rune ActivationRune;
    public Step[] Steps;
}
