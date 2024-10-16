using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawDetection : MonoBehaviour
{
    private DrawingAction _inputActions;

    bool isDrawing = false;

    Vector2 _beginningPos;
    Vector2 _currentPos;

    List<Vector2> poses = new List<Vector2>();

    [SerializeField] 

    private void OnEnable()
    {
        _inputActions = new DrawingAction();
        _inputActions.DrawingMap.Draw.Enable();
    }

    public void OnDraw(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug
            poses.Clear();


            isDrawing = true;
            _beginningPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());

            //Debug
            poses.Add(_beginningPos);

        }
        else if (context.canceled)
        {
            isDrawing = false;
        }
    }

    private void Update()
    {
        if (isDrawing)
        {
            _currentPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());

            //Debug
            poses.Add(_currentPos);
        }
    }

    //Debug
    private void OnDrawGizmos()
    {
        for (int i = 0; i < poses.Count; i++)
        {
            if(i != 0)
            {
                Gizmos.DrawLine(poses[i - 1], poses[i]);
            }
            
        }
    }
}
