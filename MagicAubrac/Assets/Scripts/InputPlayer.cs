using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using IIMEngine.SFX;

public class InputPlayer : MonoBehaviour
{
    private DrawingAction _inputActions;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Tireuse _tireuse;
    [SerializeField] private float _timerPulled;
    [HorizontalLine]
    [SerializeField] private InputJoycon _joycon;
    [HorizontalLine]
    [Header("Rune drawing")]
    [SerializeField] DrawTablet _tablet;
    [SerializeField] Material _materialBackgroundTablet;
    [SerializeField] float _maxValueMaterial = 5f;
    [SerializeField] float _speedPowerTransition = 40f;
    [SerializeField] string _tagVignettePower = "_VignettePower";
    int _indexVignettePower = 0;
    Coroutine _routineBackgroundTransition;

    ScoreDisplay _scoreUI;

    public event Action OnDrinkSucceeded;
    public event Action OnDrinkFailed;
    public event Action OnDrinkRunesOnly;
    public event Action OnDrinkTasteOnly;
    public event Action<Recipe> OnDrinkFinished;

    [Foldout("Audio")]
    [SerializeField] string clipStartRune;
    [Foldout("Audio")]
    [SerializeField] string clipSendRune;
    [Foldout("Audio")]
    [SerializeField] string clipGoodCocktail;
    [Foldout("Audio")]
    [SerializeField] string clipBadCocktail;
    [Foldout("Audio")]
    [SerializeField] string clipLeverPulled;
    private void Awake()

    {
        _scoreUI = FindObjectOfType<ScoreDisplay>();
        _inputActions = new DrawingAction();
        _inputActions.DrawingMap.DrinkPour1.Enable();
        _inputActions.DrawingMap.DrinkPour2.Enable();
        _inputActions.DrawingMap.DrinkPour3.Enable();
        _inputActions.DrawingMap.DrinkSelectA1.Enable();
        _inputActions.DrawingMap.DrinkSelectA2.Enable();
        _inputActions.DrawingMap.DrinkSelectA3.Enable();
        _inputActions.DrawingMap.DrinkSelectB1.Enable();
        _inputActions.DrawingMap.DrinkSelectB2.Enable();
        _inputActions.DrawingMap.DrinkSelectB3.Enable();
        _inputActions.DrawingMap.EmptyDrink.Enable();
        _inputActions.DrawingMap.Validate.Enable();
        _tablet._inputActions = _inputActions;
    }

    private void Start()
    {
        if (_joycon != null)
        {
            _joycon.OnStartShaking += OnStartShaking;
            _joycon.OnStopShaking += OnStopShaking;
            _joycon.OnShaking += OnJoyconShake;
        }
        if (_materialBackgroundTablet != null)
        {
            _indexVignettePower = _materialBackgroundTablet.shader.FindPropertyIndex(_tagVignettePower);
        }
    }

    public void OnValidation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Client currentClient = GameManager.ClientsManager?.CurrentClient;
            
            if (currentClient != null)
            {
                if (_shaker.IsDrawnRuneNotNull())
                {
                    OnDrinkFinished?.Invoke(GameManager.ClientsManager?.CurrentClient.Recipe);
                    if (_shaker.CompareRecipe() && _shaker.CompareRunes())
                    {
                        SFXsManager.Instance.PlaySound(clipGoodCocktail);
                        _shaker.CompletedFull++;
                        OnDrinkSucceeded?.Invoke();
                        _scoreUI?.changeScoreP(_shaker.CompletedFull);
                        currentClient.DrinkSuceeded();
                    }
                    else if (_shaker.CompareRecipe() && !_shaker.CompareRunes())
                    {
                        SFXsManager.Instance.PlaySound(clipBadCocktail);
                        _shaker.CompletedCocktail++;
                        OnDrinkTasteOnly?.Invoke();
                        _scoreUI?.changeScoreD(_shaker.CompletedCocktail);
                        currentClient.DrinkTasteOnly();
                    }
                    else if (!_shaker.CompareRecipe() && _shaker.CompareRunes())
                    {
                        SFXsManager.Instance.PlaySound(clipBadCocktail);
                        _shaker.CompletedRune++;
                        OnDrinkRunesOnly?.Invoke();
                        _scoreUI?.changeScoreR(_shaker.CompletedRune);
                        currentClient.DrinkRunesOnly();
                    }
                    else
                    {
                        SFXsManager.Instance.PlaySound(clipBadCocktail);
                        OnDrinkFailed?.Invoke();
                        currentClient.DrinkComplete();
                    }
                    _tablet.ResetRunes();
                    _tablet.ResetDrawing();
                    _shaker.EmptyShaker();
                }
                else
                {
                    SFXsManager.Instance.PlaySound(clipSendRune);
                    _tablet.ValidateRuneDrawing();
                }
            }
        }
    }

    public void OnRuneActivation(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SFXsManager.Instance.PlaySound(clipStartRune);
            _tablet.gameObject.SetActive(true);
            _tablet.enabled = true;

            //if (_materialBackgroundTablet != null)
            //{
            //    if (_routineBackgroundTransition  != null)
            //    {
            //        StopCoroutine(_routineBackgroundTransition);
            //        _routineBackgroundTransition = null;
            //    }
            //    _routineBackgroundTransition = StartCoroutine(RoutineChangeMaterialPower(0f));
            //}
        }
        if (context.canceled)
        {
            SFXsManager.Instance.StopSound(clipStartRune);
            _shaker.DeleteRune();
            _tablet.ResetDrawing();
            _tablet.enabled = false;
            _tablet.gameObject.SetActive(false);
            //if (_materialBackgroundTablet != null)
            //{
            //    if (_routineBackgroundTransition != null)
            //    {
            //        StopCoroutine(_routineBackgroundTransition);
            //        _routineBackgroundTransition = null;
            //    }
            //    _routineBackgroundTransition = StartCoroutine(RoutineChangeMaterialPower(_maxValueMaterial));
            //}
        }
    }

    IEnumerator RoutineChangeMaterialPower(float endPower)
    {
        float currentPower = _materialBackgroundTablet.GetFloat(_tagVignettePower);
        while (currentPower != endPower)
        {
            currentPower = Mathf.Lerp(currentPower, endPower, Time.deltaTime * _speedPowerTransition);
            _materialBackgroundTablet.SetFloat(_tagVignettePower, currentPower);
            yield return null; 
        }
    }

    public void OnPour(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Pour(context));
        }
        if (context.canceled)
        {
            StopAllCoroutines();
        }
    }
    public void OnChangeA(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            SFXsManager.Instance.PlaySound(clipLeverPulled);
            _tireuse.ChangeLiquid(i, true);
        }
        if (context.canceled)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            SFXsManager.Instance.StopSound(clipLeverPulled);
            _tireuse.ResetLiquid(i);
        }
    }
    public void OnChangeB(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkSelectB1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectB2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            SFXsManager.Instance.PlaySound(clipLeverPulled);
            _tireuse.ChangeLiquid(i, false);           
        }
        if (context.canceled)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            SFXsManager.Instance.StopSound(clipLeverPulled);
            _tireuse.ResetLiquid(i);
        }
    }
    public void OnEmpty(InputAction.CallbackContext context)
    {
        _shaker.EmptyShaker();
    }

    private void OnJoyconShake(float shakeDuration)
    {
        _shaker?.Shake(shakeDuration);
    }

    private void OnStartShaking()
    {
        _shaker?.StartShake();
    }

    private void OnStopShaking(float duration)
    {
        _shaker?.StopShake();
    }

    public void OnShake(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("&&");
            _shaker.Shake(6);
        }
    }
    IEnumerator Pour(InputAction.CallbackContext context)
    {
        float timer = 0;
        while (!context.canceled)
        {
            if (timer >= _timerPulled)
            {
                timer = 0;
                int i;
                if (context.action.name == "DrinkPour1")
                {
                    i = 0;
                }
                else if (context.action.name == "DrinkPour2")
                {
                    i = 1;
                }
                else
                {
                    i = 2;
                }
                _tireuse.AddLiquidToShaker(i);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    void EnableDrawing()
    {
        _inputActions.DrawingMap.Draw.Enable();
    }

    
}
