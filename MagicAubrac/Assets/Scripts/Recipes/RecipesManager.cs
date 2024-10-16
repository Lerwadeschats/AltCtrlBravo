using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] Recipe[] _recipes;

    public Recipe[] Recipes { get => _recipes; }

    private void Awake()
    {
        GameManager.RecipesManager = this;
    }

    public Recipe GetRandomRecipe()
    {
        if (_recipes.Length == 0)
            return null;
        int index = Random.Range(0, _recipes.Length);
        return _recipes[index];
    }
}
