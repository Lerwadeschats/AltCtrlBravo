using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecipes : MonoBehaviour
{
    [SerializeField] UIRecipe[] _uiRecipes;
    ClientsManager _clientsManager;
    private void Start()
    {
        _clientsManager = GameManager.ClientsManager;
        //.OnClientChange
    }
}
