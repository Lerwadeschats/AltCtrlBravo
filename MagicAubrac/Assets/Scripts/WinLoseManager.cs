using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    public event Action OnLose;

    private void Awake()
    {
        GameManager.LoseWinManager = this;
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }
}
