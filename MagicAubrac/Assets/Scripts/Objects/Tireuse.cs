using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireuse : MonoBehaviour
{
    [SerializeField]private IngredientType[] _tireuses = new IngredientType[3];

    public void AddLiquidToShaker(int tireuse)
    {
        if (_tireuses[tireuse] != IngredientType.INVALID)
        {
            //addToShaker_tireuse[tireuse]
        }
    }

    public void ChangeLiquid(int tireuse,IngredientType ingredient) 
    {
        _tireuses[tireuse] = ingredient;
    }
}
