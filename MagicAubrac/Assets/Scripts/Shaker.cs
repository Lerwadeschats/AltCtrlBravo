using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    [SerializeField] private List<RuneObject> _runes = new List<RuneObject>();
    [SerializeField] private List<Step> stepsDone = new List<Step>();
    ClientsManager _clients;
    [SerializeField] float _shakeDurationMin = 3;
    ShakerUI[] _ui;

    private bool[] _shakenAtStep = new bool[5];
    int _currentLayerCocktail;
    int _currentLayerRune;

    private void Start()
    {
        _ui = FindObjectsOfType<ShakerUI>();
        _currentLayerRune = 0;
        _currentLayerCocktail = 0;
        _clients = GameManager.ClientsManager;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        if (_currentLayerCocktail < 5)
        {
            _cocktail[_currentLayerCocktail] = ingredient;
            _ui[_ui.Length - 1 - _currentLayerCocktail].Change(ingredient);
            Step step = new Step();
            step.StepType = StepType.INGREDIENT;
            step.IngredientType = ingredient;
            stepsDone.Add(step);
            _currentLayerCocktail++;
        }
    }

    public void EmptyShaker()
    {
        for (int i = 0; i < _cocktail.Length; i++)
        {
            _cocktail[i] = IngredientType.INVALID;
        }
        _currentLayerCocktail = 0;
        for (int i = 0; i < _shakenAtStep.Length; i++)
        {
            _shakenAtStep[i] = false;
        }
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].Change(IngredientType.INVALID);
        }
        stepsDone.Clear();
    }
    public void AddToShaker(RuneObject rune)
    {
        _runes.Add(rune);

    }
    public void RemoveRune()
    {
        _runes.Clear();
    }
    public void Shake(float duration)
    {
        Debug.Log("Shakey");
        if (_currentLayerCocktail != 0 && duration >= _shakeDurationMin)
        {
            _shakenAtStep[_currentLayerCocktail - 1]=true;
            if (stepsDone[stepsDone.Count - 1].StepType != StepType.SHAKE)
            {
                Step step = new Step();
                step.StepType = StepType.SHAKE;
                stepsDone.Add(step);
            }
        }
    }

    public bool CompareRecipe()
    {
        if (_clients.CurrentClient == null)
            return false;

        Step[] steps = _clients.CurrentClient.Recipe.Steps;
        bool result = true;
        int j = 0;
        Debug.Log(steps.Length);
        Debug.Log(stepsDone.Count);
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

    
