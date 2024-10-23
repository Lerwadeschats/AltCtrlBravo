using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] InputPlayer _inputs;
    [SerializeField] float _health = 3f;
    float _currentHealth;

    public float CurrentHealth { get => _currentHealth; }

    public event Action<float> OnLoseHealth;
    public event Action OnLose;

    private void Awake()
    {
        _currentHealth = _health;
        _inputs.OnDrinkFailed += OnDrinkFailed;
        _inputs.OnDrinkRunesOnly += OnDrinkRunesOnly; ;
        _inputs.OnDrinkTasteOnly += OnDrinkTasteOnly; ;
    }

    private void OnDrinkTasteOnly()
    {
        LoseHealth(0.5f);
    }

    private void OnDrinkRunesOnly()
    {
        LoseHealth(0.5f);
    }

    private void Start()
    {
        GameManager.ClientsManager.OnClientFailed += OnDrinkFailed;
    } 

    private void OnDrinkFailed()
    {
        LoseHealth(1f);
    }

    public void LoseHealth(float healthLost)
    {
        if (_currentHealth > 0f)
        {
            _currentHealth -= healthLost;
            OnLoseHealth?.Invoke(CurrentHealth);

            if (_currentHealth <= 0f)
            {
                OnLose?.Invoke();
                GameManager.LoseWinManager?.Lose();
            }
        }
    }
}
