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
    [SerializeField] GameObject[] _ui;
     

    private bool[] _shakenAtStep = new bool[5];
    int _currentLayerCocktail;
    int _completedFull;
    int _completedCocktail;
    int _completedRune;

    public int CompletedFull { get => _completedFull; set => _completedFull = value; }
    public int CompletedCocktail { get => _completedCocktail; set => _completedCocktail = value; }
    public int CompletedRune{ get => _completedRune; set => _completedRune = value; }

    private void Start()
    {
        _currentLayerCocktail = 0;
        _clients = GameManager.ClientsManager;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        if (_currentLayerCocktail < 5)
        {
            _cocktail[_currentLayerCocktail] = ingredient;
            _ui[ _currentLayerCocktail].GetComponent<ShakerUI>().Change(ingredient);
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
            _ui[i].GetComponent<ShakerUI>().Change(IngredientType.INVALID);
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
        float r=0;
        float g=0;
        float b=0;
        int i;
        for (i=0;i  < _currentLayerCocktail;i++)
        {
            r += _ui[i].GetComponent<ShakerUI>().Image.color.r;
            g += _ui[i].GetComponent<ShakerUI>().Image.color.g;
            b += _ui[i].GetComponent<ShakerUI>().Image.color.b;
        }
        for (int j=0; j < _currentLayerCocktail; j++)
        {
            _ui[j].GetComponent<ShakerUI>().ChangeColor(new Color(r / i, g / i, b / i));
        }
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

    
