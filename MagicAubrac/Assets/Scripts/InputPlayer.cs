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
        _inputActions.DrawingMap.DrinkSelectA1.Enable();
        _inputActions.DrawingMap.DrinkSelectA2.Enable();
        _inputActions.DrawingMap.DrinkSelectA3.Enable();
        _inputActions.DrawingMap.DrinkSelectB1.Enable();
        _inputActions.DrawingMap.DrinkSelectB2.Enable();
        _inputActions.DrawingMap.DrinkSelectB3.Enable();
        _inputActions.DrawingMap.EmptyDrink.Enable();
        _inputActions.DrawingMap.Validate.Enable();

    }

    public void OnValidation(InputAction.CallbackContext context)
    {
        Debug.Log("aefzr");
        if (context.performed)
        {
            if (_shaker.CompareRecipe())
            {
                Debug.Log("le client est relativement content");
            }
            else
            {
                Debug.Log("le client ne l'est pas");
            }
            _shaker.EmptyShaker();
            _shaker.RemoveRune();
        }
    }

    public void OnRuneActivation(InputAction.CallbackContext context)
    {
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
    public void OnChangeA(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            _tireuse.ChangeLiquid(i, true);
        }
        if (context.canceled)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            _tireuse.ResetLiquid(i);
        }
    }
    public void OnChangeB(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int i;
            if (context.action.name == "DrinkSelectB1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectB2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            
            _tireuse.ChangeLiquid(i, false);
        }
        if (context.canceled)
        {
            int i;
            if (context.action.name == "DrinkSelectA1")
            {
                i = 0;
            }
            else if (context.action.name == "DrinkSelectA2")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            _tireuse.ResetLiquid(i);
        }
    }
    public void OnEmpty(InputAction.CallbackContext context)
    {
        _shaker.EmptyShaker();
    }
}
