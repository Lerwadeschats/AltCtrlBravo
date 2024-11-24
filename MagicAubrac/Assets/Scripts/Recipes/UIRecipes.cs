using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecipes : MonoBehaviour
{
    [SerializeField] GameObject[] _positions;
    [SerializeField] UIRecipe[] _uiRecipes;
    ClientsManager _clientsManager;
    private void Start()
    {
        _clientsManager = GameManager.ClientsManager;
        if (_clientsManager != null)
        {
            _clientsManager.OnClientChange += UpdateUIDelegate;
            _clientsManager.OnClientStartWaiting += UpdateUIDelegate;
        }
        InitRecipes();
    }

    private void OnDestroy()
    {
        if ( _clientsManager != null)
        {
            _clientsManager.OnClientChange -= UpdateUIDelegate;
            _clientsManager.OnClientStartWaiting -= UpdateUIDelegate;
        }
    }

    private void UpdateUIDelegate(Client client)
    {
        UpdateRecipesUI();
    }

    void InitRecipes()
    {
        for (int i = 0; i < _uiRecipes.Length; i++)
        {
            _uiRecipes[i].gameObject.SetActive(false);
        }
    }

    void UpdateRecipesUI()
    {
        int nbClientsWaiting = _clientsManager.ClientsInQueue.Count;

        for (int i = 0; i < _uiRecipes.Length; i++)
        {
            Client client = null;
            if (i < nbClientsWaiting)
            {
                client = _clientsManager.ClientsInQueue[i];
            }
            
            if (client != null && client.IsWaiting)
            {
                _uiRecipes[i].gameObject.SetActive(true);
                _uiRecipes[i].Client = _clientsManager.ClientsInQueue[i];
                Debug.Log($"{i} {_uiRecipes[i].Client} {_uiRecipes[i].Client?.RemainingWaitingDuration}");
            }
            else
            {
                _uiRecipes[i].gameObject.SetActive(false);
            }
        }
    }
}
