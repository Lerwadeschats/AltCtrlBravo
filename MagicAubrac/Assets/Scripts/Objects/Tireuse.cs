using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireuse : MonoBehaviour
{
    [SerializeField]private IngredientType[] _tireuses = new IngredientType[3];
    [SerializeField]private IngredientType[] _tireusesIngrA = new IngredientType[3];
    [SerializeField]private IngredientType[] _tireusesIngrB = new IngredientType[3];
    [SerializeField] private Shaker _shaker;
    [SerializeField] private float _timerPulled;
    private float timer=0;

    public void AddLiquidToShaker(int tireuse)
    {
        if (_tireuses[tireuse] != IngredientType.INVALID)
        {
            timer += Time.deltaTime;
            if (timer > _timerPulled)
            {
                _shaker.AddToShaker(_tireuses[tireuse]);
                timer = 0;
            }
        }
    }
    public void ResetTimer()
    {
        timer = 0;
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
        ResetTimer();
    }
    public void ResetLiquid(int tireuse)
    {
        _tireuses[tireuse] = IngredientType.INVALID;
    }
}
