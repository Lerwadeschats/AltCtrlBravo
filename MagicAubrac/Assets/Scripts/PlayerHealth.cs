using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] InputPlayer _inputs;
    [SerializeField] float _health = 3f;
    [SerializeField] float _healthStep = 0.5f;
    float _currentHealth;

    public float CurrentHealth { get => _currentHealth; }

    public event Action<float> OnLoseHealth;
    public event Action OnLose;

    private void Awake()
    {
        _currentHealth = _health;
        _inputs.OnDrinkFailed += OnDrinkFailed;
    }

    private void OnDrinkFailed()
    {
        LoseHealth();
    }

    public void LoseHealth()
    {
        if (_currentHealth > 0f)
        {
            _currentHealth -= _healthStep;
            OnLoseHealth?.Invoke(CurrentHealth);

            if (_currentHealth == 0f)
            {
                OnLose?.Invoke();
                GameManager.LoseWinManager?.Lose();
            }
        }
    }
}
