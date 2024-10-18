using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireuse : MonoBehaviour
{
    [SerializeField]private IngredientType[] _tireuses = new IngredientType[3];
    [SerializeField]private IngredientType[] _tireusesIngrA = new IngredientType[3];
    [SerializeField]private IngredientType[] _tireusesIngrB = new IngredientType[3];
    [SerializeField] private Shaker _shaker;

    public void AddLiquidToShaker(int tireuse)
    {
        if (_tireuses[tireuse] != IngredientType.INVALID)
        {
            _shaker.AddToShaker(_tireuses[tireuse]);
        }
    }

    public void ChangeLiquid(int tireuse,bool isA) 
    {
        if (isA)
        {
            _tireuses[tireuse] = _tireusesIngrA[tireuse];
        }
        else
        {
            _tireuses[tireuse] = _tireusesIngrB[tireuse];
        }
    }
    public void ResetLiquid(int tireuse)
    {
        _tireuses[tireuse] = IngredientType.INVALID;
    }
}
