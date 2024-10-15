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
    public StepType StepType; //TODO Modify to separate shake and ingredient
    public float ShakeDuration;
    public IngredientType IngredientType;
}