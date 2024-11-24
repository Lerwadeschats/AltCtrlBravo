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
            _clientsManager.OnClientStartWaiting += UpdateUIDelegateStartWaiting;
        }
        InitRecipes();
    }

    private void OnDestroy()
    {
        if ( _clientsManager != null)
        {
            _clientsManager.OnClientChange -= UpdateUIDelegate;
            _clientsManager.OnClientStartWaiting -= UpdateUIDelegateStartWaiting;
        }
    }

    private void UpdateUIDelegateStartWaiting(Client client)
    {
        UpdateRecipesUI();
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
            }
            else
            {
                _uiRecipes[i].gameObject.SetActive(false);
                _uiRecipes[i].Client = client;
            }
        }
    }
}
