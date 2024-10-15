using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSettings : MonoBehaviour
{
    [SerializeField] private int _nbPortionsInRecipe = 5;

    [SerializeField] private float _quantityPerPortion = 20f;

    public int NbPortionsInRecipe { get => _nbPortionsInRecipe; }
    public float QuantityPerPortion { get => _quantityPerPortion; }
}
