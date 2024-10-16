using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    int currentLayer;
    private void Start()
    {
        currentLayer = 0;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        _cocktail[currentLayer] = ingredient;
    }

    public void EmptyShaker()
    {
        for(int i = 0; i < _cocktail.Length; i++)
        {
            _cocktail[i] = IngredientType.INVALID;
        }
        currentLayer = 0;
    }
}
