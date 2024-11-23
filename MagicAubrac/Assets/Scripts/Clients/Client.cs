using System;
using System.Collections;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float _speed = 30f; 

    [SerializeField] private float _floatingPointMovement = 0.001f;
    [SerializeField] private float _heightPeriodMovement = 0.2f;
    [SerializeField] private float _widthPeriodMovement = 0.2f;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _orderLayerFinished = 10;

    private float _waitingDuration;
    private float _remainingWaitingDuration;

    private Coroutine _coroutineWait; // Will be stopped if go is destroyed
    private Coroutine _coroutineMovement; // Will be stopped if go is destroyed
    private MenuManager _menuManager;
    private bool _isComplete = false;
    private bool _isTutorial;
    private bool _hasStartedWaiting = false;

    public float WaitingDuration
    {
        get => _waitingDuration;
    }

    public float RemainingWaitingDuration { 
        get => _remainingWaitingDuration;
    }

    public GameObject EndPosition { get; set; }
    public Recipe Recipe { get; private set; }
    public bool IsTutorial { get => _isTutorial; }
    public bool IsWaiting { get => _hasStartedWaiting; }

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
    public void LoadClient(Recipe recipe, float waitingDuration,  bool isTutorial = false)
    {
        _waitingDuration = waitingDuration;
        Recipe = recipe;
        _isTutorial = isTutorial;
    }

    //When client is visible
    public void ClientStartWaiting()
    {
        if (!_isTutorial)
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
            if (_menuManager != null)
            {
                yield return new WaitUntil(() => !_menuManager.IsInMenu);
            }
            _remainingWaitingDuration -= Time.deltaTime;
            yield return null;
        }
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
        DrinkComplete();
    }
    public void DrinkRunesOnly()
    {
        // jsp
        OnDrinkFailed?.Invoke(this);
        DrinkComplete();
    }
    public void DrinkTasteOnly()
    {
        // jsp
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
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sortingOrder = _orderLayerFinished;
        }
        OnClientCompleted?.Invoke(this);
        Debug.Log("MOVE UPDATE");
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
            if (_menuManager != null)
            {
                yield return new WaitUntil(() => !_menuManager.IsInMenu);
            }

            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _speed);
            //Debug.Log($"Distance {Mathf.Abs(transform.position.x - destination.x)}");
            yield return null;
        }
        //Debug.Log("COUCOU");
        OnPositionReached?.Invoke();
    }

    private void PositionReached()
    {
        if (!_hasStartedWaiting)
        {
            _hasStartedWaiting = true;
            //Debug.Log("START WAITING");
            ClientStartWaiting();

        }
        if (_isComplete)
        {
            Destroy(gameObject);
        }
    }

    public string GetDebugString()
    {
        return Recipe?.GetDebugString();
    }

    internal void UpdateOrder(int index)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sortingOrder = index;
        }
    }
}
