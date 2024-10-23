using System;
using System.Collections;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float _speed = 30f; 
    [SerializeField] private float _waitingDuration = 10f;

    [SerializeField] private float _floatingPointMovement = 0.001f;
    [SerializeField] private float _heightPeriodMovement = 0.2f;
    [SerializeField] private float _widthPeriodMovement = 0.2f;
    private float _timerMovement = 0f;
    
    private float _remainingWaitingDuration;
    private Coroutine _coroutineWait; // Will be stopped if go is destroyed
    private Coroutine _coroutineMovement; // Will be stopped if go is destroyed
    private MenuManager _menuManager;
    private bool _isComplete = false;

    public float RemainingWaitingDuration { 
        get => _remainingWaitingDuration;
    }

    public GameObject EndPosition { get; set; }
    public Recipe Recipe { get; private set; }
    public float WaitingDuration { get => _waitingDuration;}

    private bool _waitEndlessly;

    public event Action<Client> OnClientCompleted;
    public event Action<Client> OnDrinkTookTooLong;
    public event Action<Client> OnDrinkFailed; //Any kind of failure
    public event Action OnPositionReached;

    private void Start()
    {
        _menuManager = GameManager.MenuManager;
        OnPositionReached += PositionReached;
    }

    //When client is instantiate in list
    public void LoadClient(Recipe recipe, bool waitEndlessly = false)
    {
        Recipe = recipe;
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
        DrinkTooLateFailed();
    }

    public void DrinkTooLateFailed()
    {
        OnDrinkFailed?.Invoke(this);
        OnDrinkTookTooLong?.Invoke(this);
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
        OnDrinkFailed?.Invoke(this);
        DrinkComplete();
    }
    public void DrinkTasteOnly()
    {
        // jsp
        Debug.Log("FlopRunes");
        OnDrinkFailed?.Invoke(this);
        DrinkComplete();
    }

    public void DrinkFullyFailed()
    {
        OnDrinkFailed?.Invoke(this);
        DrinkComplete();
    }

    //Use if drink is finished 
    public void DrinkComplete()
    {
        OnClientCompleted?.Invoke(this);
        MoveTo(EndPosition == null ? Vector3.zero:EndPosition.transform.position);
        _isComplete = true;
    }

    public void MoveTo(Vector3 destination)
    {
        if (_coroutineMovement != null)
        {
            StopCoroutine(_coroutineMovement);
            _coroutineMovement = null;
        }
        _coroutineMovement = StartCoroutine(MoveToRoutine(destination));
    }

    IEnumerator MoveToRoutine(Vector3 destination)
    {
        while (Mathf.Abs(transform.position.x - destination.x) > _floatingPointMovement)
        {
            yield return new WaitUntil(() => _menuManager == null || !_menuManager.IsInMenu);

            _timerMovement += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _speed);

            yield return null;
        }

        OnPositionReached?.Invoke();
    }

    private void PositionReached()
    {
        if (_isComplete)
        {
            Destroy(gameObject);
        }
    }

    public string GetDebugString()
    {
        return Recipe?.GetDebugString();
    }
}
