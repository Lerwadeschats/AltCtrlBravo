using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    [SerializeField] private List<Client> _clientsPossible;
    [SerializeField] private int _nbClientsShown = 3;
    
    public List<Client> ClientsInQueue { get; private set; }
    public Client CurrentClient { get; private set; }

    private void OnValidate()
    {
        //A TEST SI PAS CHIANT
        if (_nbClientsShown < 1) {
            _nbClientsShown = 1;
        }
    }

    private void Awake()
    {
        InitClientsList();
    }

    void InitClientsList()
    {
        ClientsInQueue = new List<Client>(_nbClientsShown);
        for (int i = 0; i < _nbClientsShown; i++)
        {
            Client client = GetRandomClient();
            if (client != null)
            {
                ClientsInQueue.Add(client);
            }
        }
        if (ClientsInQueue.Count > 0)
        {
            CurrentClient = ClientsInQueue[0];
        }
    }

    Client GetRandomClient()
    {
        if (_clientsPossible.Count == 0)
            return null;
        
        int index = Random.Range(0, _clientsPossible.Count);
        return _clientsPossible[index];
    }

    public void ChangeClient()
    {
        ClientsInQueue.RemoveAt(0);
        Client client = GetRandomClient();
        if (client != null)
        {
            ClientsInQueue.Add(client);
        }
        CurrentClient = ClientsInQueue[0];
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
