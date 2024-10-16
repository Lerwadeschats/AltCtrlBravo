using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private IngredientType[] _cocktail = new IngredientType[5];
    [SerializeField] private Rune[] _runes = new Rune[3];
    int currentLayerCocktail;
    int currentLayerRune;
    private void Start()
    {
        currentLayerRune = 0;
        currentLayerCocktail = 0;
    }
    // je n'ai fait que le remplissage pour des raisons de j'en avais besoins il faut completer le tout plus tard :)

    public void AddToShaker(IngredientType ingredient)
    {
        _cocktail[currentLayerCocktail] = ingredient;
        currentLayerCocktail++;
    }

    public void EmptyShaker()
    {
        for(int i = 0; i < _cocktail.Length; i++)
        {
            _cocktail[i] = IngredientType.INVALID;
        }
        currentLayerCocktail = 0;
    }
    public void AddToShaker(Rune rune)
    {
        _runes[currentLayerRune] = rune;
        currentLayerRune++;
    }
    public void RemoveRune()
    {
        Debug.Log("oui");
        for (int i = 0; i < _runes.Length; i++)
        {
            _runes[i] = Rune.NONE;
        }
        currentLayerRune = 0;
    }

}
