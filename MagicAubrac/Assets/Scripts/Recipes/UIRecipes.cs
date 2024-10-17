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
        _clientsManager.OnClientChange += UpdateUIDelegate;
        _clientsManager.OnClientWalkInForeground += UpdateUIDelegate;
        UpdateRecipesUI();
    }

    private void UpdateUIDelegate(Client client)
    {
        UpdateRecipesUI();
    }

    void UpdateRecipesUI()
    {
        int nbClientsWaiting = _clientsManager.ClientsInQueue.Count;
        for (int i = 0; i < _uiRecipes.Length; i++)
        {
            if (i < nbClientsWaiting)
            {
                _uiRecipes[i].gameObject.SetActive(true);
                _uiRecipes[i].Client = _clientsManager.ClientsInQueue[i];
            }
            else
            {
                _uiRecipes[i].gameObject.SetActive(false);
            }
        }
    }
}
