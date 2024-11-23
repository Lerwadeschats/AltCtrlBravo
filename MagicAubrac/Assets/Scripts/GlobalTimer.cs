using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviour
{
    [SerializeField] private float _totalTimer;
    [SerializeField] Slider _topSlider;
    [SerializeField] Slider _bottomSlider;

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float currentTimer = _totalTimer;

        while (currentTimer > 0)
        {
            float percentage = currentTimer / _totalTimer;
            _topSlider.value = percentage;
            _bottomSlider.value = 1-percentage;
            currentTimer -= Time.deltaTime;
            print(currentTimer);
            yield return null;
        }
        GameManager.LoseWinManager?.Lose();
    }
}
