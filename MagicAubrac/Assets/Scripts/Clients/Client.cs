using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] float _waitingDuration = 10f;
    private Recipe _recipe;

    //Pas fini
    public float CurrentWaitingDuration { get { return _waitingDuration; } }

    //When client is instantiate in list
    public void LoadClient()
    {
        _recipe = GameManager.RecipesManager.GetRandomRecipe();
    }
    //When client is visible
    public void StartClient()
    {

    }

    public string GetDebugString()
    {
        return _recipe?.GetDebugString();
    }
}
