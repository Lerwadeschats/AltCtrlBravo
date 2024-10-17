using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireuse : MonoBehaviour
{
    [SerializeField]private IngredientType[] _tireuses = new IngredientType[3];
    [SerializeField] private Shaker _shaker;

    public void AddLiquidToShaker(int tireuse)
    {
        Debug.Log(_tireuses[tireuse]);
        if (_tireuses[tireuse] != IngredientType.INVALID)
        {
            _shaker.AddToShaker(_tireuses[tireuse]);
        }
    }

    public void ChangeLiquid(int tireuse,IngredientType ingredient) 
    {
        Debug.Log(ingredient);
        _tireuses[tireuse] = ingredient;
    }
}
