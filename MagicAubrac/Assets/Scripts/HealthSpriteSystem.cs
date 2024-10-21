using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpriteSystem : MonoBehaviour
{
    [SerializeField] HealthSprite[] _healthSprites;
    [SerializeField] PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth.OnLoseHealth += OnPlayerLoseHealth;
    }

    private void OnPlayerLoseHealth(float newHealth)
    {
        int indexSprite = Mathf.FloorToInt(newHealth);
        float decimalPart = newHealth % 1f;

        if (indexSprite < _healthSprites.Length)
        {
            _healthSprites[indexSprite].ChangeSprite(decimalPart);
        }
    }

}
