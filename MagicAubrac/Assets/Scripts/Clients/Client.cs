using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Recipe _recipe;

    public void LoadClient()
    {
        _recipe = GameManager.RecipesManager.GetRandomRecipe();
    }

    public string GetDebugString()
    {
        return _recipe?.GetDebugString();
    }
}
