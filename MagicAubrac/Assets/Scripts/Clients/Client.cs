using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float _waitingDuration = 10f;
    private float _currentWaitingDuration;
    private Coroutine _coroutineWait; // Will be stopped if go is destroyed

    public float CurrentWaitingDuration { 
        get => _currentWaitingDuration;
    }
    public Recipe Recipe { get; private set; }
    public float WaitingDuration { get => _waitingDuration;}

    //When client is instantiate in list
    public void LoadClient()
    {
        Recipe = GameManager.RecipesManager.GetRandomRecipe();
    }

    //When client is visible
    public void StartClient()
    {
        _coroutineWait = StartCoroutine(RoutineWaitRecipe());
    }

    IEnumerator RoutineWaitRecipe()
    {
        _currentWaitingDuration = 0f;
        while (_currentWaitingDuration < _waitingDuration)
        {
            _currentWaitingDuration += Time.deltaTime;
            yield return null;
        }
    }

    public string GetDebugString()
    {
        return Recipe?.GetDebugString();
    }
}
