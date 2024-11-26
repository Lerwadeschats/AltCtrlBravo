using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IIMEngine.SFX;
using DG.Tweening;
using System.Collections;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    [SerializeField] private RuneObject _rune;
    [SerializeField] private List<Step> stepsDone = new List<Step>();
    ClientsManager _clients;
    [SerializeField] float _shakeDurationMin = 3;
    [SerializeField] GameObject _UIshakerGO;
    [SerializeField] GameObject _ui;
    [SerializeField] LiquidSpawner _liquidSpawner;

    [Foldout("Audio")]
    [SerializeField] string clipPour;

    [Foldout("Audio")]
    [SerializeField] string clipEmpty;

    private bool[] _shakenAtStep = new bool[5];
    int _currentLayerCocktail;
    int _completedFull;
    int _completedCocktail;
    int _completedRune;
    Sequence _sequenceShake;

    public int CompletedFull { get => _completedFull; set => _completedFull = value; }
    public int CompletedCocktail { get => _completedCocktail; set => _completedCocktail = value; }
    public int CompletedRune{ get => _completedRune; set => _completedRune = value; }

    public event Action OnShakeStarted;
    public event Action OnShakePaused;

    private void Start()
    {
        //_sequenceShake = DOTween.Sequence();
        //_sequenceShake.Append(_UIshakerGO.transform.DOShakePosition(_shakeDuration, _shakeForcePosition));
        //_sequenceShake.Join(_UIshakerGO.transform.DOShakeRotation(_shakeDuration, _shakeForceRotation,10,20));
        //_sequenceShake.SetLoops(-1);
        //_sequenceShake.Pause();

        _currentLayerCocktail = 0;
        _clients = GameManager.ClientsManager;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        SFXsManager.Instance?.PlaySound(clipPour);
        if (_currentLayerCocktail < 5)
        {
            _cocktail[_currentLayerCocktail] = ingredient;
            Color c=_ui.GetComponent<ShakerUI>().GetColor(ingredient);
            _liquidSpawner.Serve(c);
            Step step = new Step();
            step.StepType = StepType.INGREDIENT;
            step.IngredientType = ingredient;
            stepsDone.Add(step);
            _currentLayerCocktail++;
        }
    }

    public void EmptyShaker()
    {
        SFXsManager.Instance?.PlaySound(clipEmpty);
        for (int i = 0; i < _cocktail.Length; i++)
        {
            _cocktail[i] = IngredientType.INVALID;
        }
        _currentLayerCocktail = 0;
        for (int i = 0; i < _shakenAtStep.Length; i++)
        {
            _shakenAtStep[i] = false;
        }
        _liquidSpawner.KillWater();
        stepsDone.Clear();
    }
    public void AddToShaker(RuneObject rune)
    {
        _rune = rune;

    }

    public bool IsDrawnRuneNotNull()
    {
        if(_rune != null)
        {
            return true;
        }
        return false;
    }
    public void DeleteRune()
    {
        _rune = null;

        
    }
    public void Shake(float duration)
    {
        if (_currentLayerCocktail != 0 && duration >= _shakeDurationMin)
        {
            Debug.Log("Shakey");
            _shakenAtStep[_currentLayerCocktail - 1]=true;
            if (stepsDone[stepsDone.Count - 1].StepType != StepType.SHAKE)
            {
                Step step = new Step();
                step.StepType = StepType.SHAKE;
                stepsDone.Add(step);
            }
            List<IngredientType> ingredients = new List<IngredientType>();
            for (int j = 0; j < _currentLayerCocktail; j++)
            {
                ingredients.Add(_cocktail[j]);
            }
            Color c = _ui.GetComponent<ShakerUI>().GetMixColor(ingredients);
            _liquidSpawner.Mix(c);
        }
        
    }

    public void StartShake()
    {
        OnShakeStarted?.Invoke();
    }

    public void StopShake()
    {
        OnShakePaused?.Invoke();
    }

    public bool CompareRecipe()
    {
        if (_clients.CurrentClient == null)
            return false;

        Step[] steps = _clients.CurrentClient.Recipe.Steps;
        bool result = true;
        int j = 0;
        
        if (steps.Length != stepsDone.Count)
        {
            return false;
        }
        for (int i = 0; i < steps.Length; i++)
        {
            if (steps[i].StepType == StepType.INGREDIENT)
            {
                if (stepsDone[i].IngredientType != steps[i].IngredientType)
                {
                    result = false;
                    break;
                }
                j++;
            }
            if (steps[i].StepType == StepType.SHAKE)
            {
                if (stepsDone[i].StepType != StepType.SHAKE)
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }

    public bool CompareRunes()
    {
        RuneObject runeRecipe = _clients.CurrentClient.Recipe.ActivationRune;
        if(_rune == null || runeRecipe != _rune)
        {
            return false;
        }
        return true;
        
    }
}

    
