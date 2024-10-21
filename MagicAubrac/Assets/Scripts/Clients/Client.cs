using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float _waitingDuration = 10f;
    private float _remainingWaitingDuration;
    private Coroutine _coroutineWait; // Will be stopped if go is destroyed
    private MenuManager _menuManager;

    public float RemainingWaitingDuration { 
        get => _remainingWaitingDuration;
    }

    public Recipe Recipe { get; private set; }
    public float WaitingDuration { get => _waitingDuration;}

    public bool _waitEndlessly;

    public event Action<Client> OnClientCompleted;

    private void Start()
    {
        _menuManager = GameManager.MenuManager;
    }

    //When client is instantiate in list
    public void LoadClient(bool waitEndlessly = false)
    {
        Recipe = GameManager.RecipesManager.GetRandomRecipe();
        _waitEndlessly = waitEndlessly;
    }

    //When client is visible
    public void ClientStartWaiting()
    {
        if (!_waitEndlessly)
        {
            if (_coroutineWait != null)
            {
                StopCoroutine(_coroutineWait);
                _coroutineWait = null;
            }
            _remainingWaitingDuration = _waitingDuration;
            _coroutineWait = StartCoroutine(RoutineWaitRecipe());
        }
    }

    IEnumerator RoutineWaitRecipe()
    {
        while (_remainingWaitingDuration > 0f)
        {
            yield return new WaitUntil(() => _menuManager == null || !_menuManager.IsInMenu);
            _remainingWaitingDuration -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Wait for too long");
        DrinkFailed();
    }

    public void DrinkFailed()
    {
        // - vie
        Debug.Log("Flop");
        DrinkComplete();
    }

    public void DrinkSuceeded()
    {
        // + score
        Debug.Log("Yes");
        DrinkComplete();
    }
    public void DrinkRunesOnly()
    {
        // jsp
        Debug.Log("FlopDrink");
        DrinkComplete();
    }
    public void DrinkTasteOnly()
    {
        // jsp
        Debug.Log("FlopRunes");
        DrinkComplete();
    }

    //Use if drink is finished 
    public void DrinkComplete()
    {
        OnClientCompleted?.Invoke(this);
        Destroy(gameObject);
        //StopCoroutine(_coroutineWait);
        //_coroutineWait = null;
    }

    public string GetDebugString()
    {
        return Recipe?.GetDebugString();
    }
}
