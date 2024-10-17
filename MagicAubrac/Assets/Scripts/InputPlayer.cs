using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    private DrawingAction _inputActions;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Tireuse _tireuse;
    [SerializeField] private NFCID _ingrNFC;



    private void Awake()

    {
        _inputActions = new DrawingAction();
        _inputActions.DrawingMap.DrinkPour1.Enable();
        _inputActions.DrawingMap.DrinkPour2.Enable();
        _inputActions.DrawingMap.DrinkPour3.Enable();
        _inputActions.DrawingMap.DrinkSelect1.Enable();
        _inputActions.DrawingMap.DrinkSelect2.Enable();
        _inputActions.DrawingMap.DrinkSelect3.Enable();
        _inputActions.DrawingMap.EmptyDrink.Enable();
        _inputActions.DrawingMap.Validate.Enable();

    }

    public void OnValidation(InputAction.CallbackContext context)
    {
        Debug.Log("a");
        if (context.performed)
        {
            Debug.Log("à faire quand on a finit le truc de base");
        }
    }

    public void OnRuneActivation(InputAction.CallbackContext context)
    {
        Debug.Log("a");

        if (context.started)
        {
            Debug.Log("pareil");
        }
        if (context.canceled)
        {
            _shaker.RemoveRune();
        }
    }
    public void OnPour(InputAction.CallbackContext context)
    {
        Debug.Log("a");
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkPour1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkPour2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            _tireuse.AddLiquidToShaker(i);
        }       
    }
    public void OnChange(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkSelect1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelect2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            IngredientType ingredient = _ingrNFC.GetIngredient();
            if (ingredient != IngredientType.INVALID)
            {
                _tireuse.ChangeLiquid(i, ingredient);
            }
        }
    }
    public void OnEmpty(InputAction.CallbackContext context)
    {
        Debug.Log("a");
        _shaker.EmptyShaker();
    }
}
