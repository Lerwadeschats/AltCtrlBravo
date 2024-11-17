using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IIMEngine.SFX;
using DG.Tweening;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    [SerializeField] private List<RuneObject> _runes = new List<RuneObject>();
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

    [Foldout("Shake data")]
    [SerializeField] float _shakeForcePosition = 0.2f;
    [SerializeField] float _shakeForceRotation = 0.2f;
    [SerializeField] float _shakeDuration = 1f;

    private bool[] _shakenAtStep = new bool[5];
    int _currentLayerCocktail;
    int _completedFull;
    int _completedCocktail;
    int _completedRune;
    Sequence _sequenceShake;

    public int CompletedFull { get => _completedFull; set => _completedFull = value; }
    public int CompletedCocktail { get => _completedCocktail; set => _completedCocktail = value; }
    public int CompletedRune{ get => _completedRune; set => _completedRune = value; }

    private void Start()
    {
        _sequenceShake = DOTween.Sequence();
        _sequenceShake.Append(_UIshakerGO.transform.DOShakePosition(_shakeDuration, _shakeForcePosition));
        _sequenceShake.Join(_UIshakerGO.transform.DOShakeRotation(_shakeDuration, _shakeForceRotation,10,20));
        _sequenceShake.SetLoops(-1);
        _sequenceShake.Pause();

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
        stepsDone.Clear();
    }
    public void AddToShaker(RuneObject rune)
    {
        _runes.Add(rune);

    }

    public bool IsDrawnRunesFull()
    {
        if(_runes.Count == 3)
        {
            return true;
        }
        return false;
    }
    public void RemoveRune()
    {
        _runes.Clear();

        
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
        _sequenceShake.Play();
    }

    public void StopShake()
    {
        _sequenceShake.Pause();
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
        RuneObject[] runes = _clients.CurrentClient.Recipe.ActivationRunes;
        if(_runes.Count < runes.Length)
        {
            return false;
        }
        foreach (RuneObject drawRune in _runes)
        {
            if (!runes.Contains(drawRune))
            {
                return false;
            }
        }
        return true;
        
    }
}

    
