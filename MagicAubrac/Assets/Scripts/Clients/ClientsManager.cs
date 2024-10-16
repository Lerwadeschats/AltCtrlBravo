using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _clientsPossible;
    [SerializeField] private List<GameObject> _clientsPositions;
    [SerializeField] private GameObject _remainingQueuePosition;
    [SerializeField] private int _nbClientsShown = 3;
    [SerializeField] private int _nbClientsMax = 8;
    [SerializeField] private float _startingDurationBetweenClients = 1f; //Currently only duration
    
    public List<Client> ClientsInQueue { get; private set; }
    public Client CurrentClient { get; private set; }

    public event Action<Client> OnNewClientInList;
    public event Action<Client> OnClientChange;

    private void OnValidate()
    {
        if (_nbClientsShown < 1) {
            _nbClientsShown = 1;
        }
        if (_nbClientsMax < _nbClientsShown)
        {
            _nbClientsMax = _nbClientsShown;
        }
    }

    private void Awake()
    {
        GameManager.ClientsManager = this;
        ClientsInQueue = new List<Client>(_nbClientsShown);
    }


    GameObject GetRandomClient()
    {
        if (_clientsPossible == null && _clientsPossible.Count == 0)
            return null;
        
        int index = UnityEngine.Random.Range(0, _clientsPossible.Count);
        return _clientsPossible[index];
    }

    public void ChangeClient()
    {
        if (ClientsInQueue != null && ClientsInQueue.Count > 0)
        {
            Destroy(ClientsInQueue[0].gameObject);
            ClientsInQueue.RemoveAt(0);
            if (ClientsInQueue.Count > 0)
            {
                CurrentClient = ClientsInQueue[0];
            } else
            {
                CurrentClient = null;
            }

            UpdatePositionsClients();
            OnClientChange?.Invoke(CurrentClient);

            //Start timer last client
        }
    }

    private void UpdatePositionsClients()
    {
        for (int i = 0; i < ClientsInQueue.Count && i < _nbClientsShown && i < _clientsPositions.Count; i++)
        {
            ClientsInQueue[i].transform.position = _clientsPositions[i].transform.position;
        }
    }

    public void AddNewClient()
    {
        if (ClientsInQueue.Count < _nbClientsMax)
        {
            GameObject newClientPrefab = GetRandomClient();
            Vector3 position;
            if (ClientsInQueue.Count < _nbClientsShown && 
                ClientsInQueue.Count < _clientsPositions.Count)
            {
                position = _clientsPositions[ClientsInQueue.Count].transform.position;
            } else
            {
                position = _remainingQueuePosition.transform.position;
            }
            GameObject newClientGO = Instantiate(newClientPrefab, position, Quaternion.identity);
            Client newClient = newClientGO.GetComponent<Client>();
            newClient.LoadClient();
            ClientsInQueue.Add(newClient);

            OnNewClientInList?.Invoke(newClient);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeClient();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddNewClient();
        }
    }

    private void OnGUI()
    {
        string guiOutput = "Client: \n";
        if (ClientsInQueue != null)
        {
            foreach (Client client in ClientsInQueue)
            {
                guiOutput += client.GetDebugString() + "\n";
            }
        }
        GUILayout.Label(guiOutput);
    }
}
