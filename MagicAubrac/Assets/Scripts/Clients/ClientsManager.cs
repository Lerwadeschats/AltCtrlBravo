using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    private Coroutine _coroutineSpawnClient;

    [SerializeField] private List<GameObject> _clientsPossible;
    [SerializeField] private List<GameObject> _clientsPositions;
    [SerializeField] private GameObject _remainingQueuePosition;
    [SerializeField] private int _nbClientsShown = 3;
    [SerializeField] private int _nbClientsMax = 8;
    [SerializeField] private float _startingDurationBetweenClients = 1f; //Currently only duration
    public List<Client> ClientsInQueue { get; private set; } // TO SPLIT IN TWO LISTS ?
    public Client CurrentClient { get; private set; }

    public event Action<Client> OnNewClientInList;
    public event Action<Client> OnClientChange;

    [Header("Debug")]
    [SerializeField] private bool _activateAutoFill = true;


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

    private void Start()
    {
        //First client
        AddNewClient();
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

            if (ClientsInQueue.Count < _nbClientsMax 
                && _coroutineSpawnClient == null 
                && _activateAutoFill)
                _coroutineSpawnClient = StartCoroutine(RoutineAddClient());
        }
    }

    public IEnumerator RoutineAddClient()
    {
        yield return new WaitForSeconds(_startingDurationBetweenClients);
        AddNewClient();
        if (ClientsInQueue.Count < _nbClientsMax)
        {
            _coroutineSpawnClient = StartCoroutine(RoutineAddClient());
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
            Debug.Log("Client finished");
            ChangeClient();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Client added");
            AddNewClient();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Queue is filled automaticcaly");
            if (ClientsInQueue.Count < _nbClientsMax && _coroutineSpawnClient == null) //On start pas le timer direct (peut-�tre � changer)
                _coroutineSpawnClient = StartCoroutine(RoutineAddClient());
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
