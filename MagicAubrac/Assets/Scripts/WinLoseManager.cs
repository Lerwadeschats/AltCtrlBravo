using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    public event Action OnLose;

    public void Lose()
    {
        OnLose?.Invoke();
    }
}
