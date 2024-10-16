using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    LEMON,
    ORANGE,
    WINE,
    VODKA
}

public enum StepType
{
    SHAKE,
    INGREDIENT
}

[Serializable]
public class Step 
{
    public StepType StepType;
    [ShowIf("StepType", StepType.SHAKE)]
    [AllowNesting]
    public float ShakeDuration = 3f;
    
    [ShowIf("StepType", StepType.INGREDIENT)]
    [AllowNesting]
    public IngredientType IngredientType;
}