using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    [SerializeField] private Rune[] _runes = new Rune[3];
    ClientsManager _clients;
    private bool[] _shakenAtStep=new bool[5];
    int _currentLayerCocktail;
    int _currentLayerRune;
    private void Start()
    {
        _currentLayerRune = 0;
        _currentLayerCocktail = 0;
        _clients = GameManager.ClientsManager;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        _cocktail[_currentLayerCocktail] = ingredient;
        _currentLayerCocktail++;
    }

    public void EmptyShaker()
    {
        for(int i = 0; i < _cocktail.Length; i++)
        {
            _cocktail[i] = IngredientType.INVALID;
        }
        _currentLayerCocktail = 0;
        for (int i =0;i< _shakenAtStep.Length;i++) 
        {
            _shakenAtStep[i] = false;
        }
    }
    public void AddToShaker(Rune rune)
    {
        _runes[_currentLayerRune] = rune;
        _currentLayerRune++;
    }
    public void RemoveRune()
    {
        Debug.Log("oui");
        for (int i = 0; i < _runes.Length; i++)
        {
            _runes[i] = Rune.NONE;
        }
        _currentLayerRune = 0;
    }
    public void Shake()
    {
        if (_currentLayerCocktail != 0)
        {
            _shakenAtStep[_currentLayerCocktail - 1]=true;
        }
    }

    public bool CompareReceipe()
    {
        Step[] steps = _clients.CurrentClient.Recipe.Steps;
        bool result=true;
        int j = 0;
        for (int i = 0; i < steps.Length; i++)
        {
            if (steps[i].StepType == StepType.INGREDIENT)
            {
                if(_cocktail[j]!= steps[i].IngredientType)
                {
                    result = false;
                    break;
                }
                j++;
            }
            if(steps[i].StepType == StepType.SHAKE)
            {
                if (_shakenAtStep[j - 1] == false) 
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }
}
