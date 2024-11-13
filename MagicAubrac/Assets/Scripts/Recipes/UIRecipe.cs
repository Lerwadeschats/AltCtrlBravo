using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIRecipe : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float _shakeForcePosition = 1f;
    [SerializeField] float _shakeForceRotation = 15f;

    [SerializeField] Slider _slider;
    [SerializeField] Gradient _gradient;
    [SerializeField] Image _fillAreaImage;

    private Client _client;
    private Coroutine _coroutineTimer;
    private bool _hasStartedShaking = false;
    private float _thresholdStartShaking = 0.2f;

    public Client Client {
        get => _client;
        set {
            _client = value;
            UpdateUIRecipe();
        }
    }

    private void UpdateUIRecipe()
    {
        if (Client != null)
        {
            _image.sprite = Client.Recipe.UIRecipeSprite;
            if (_coroutineTimer != null)
            {
                StopCoroutine(_coroutineTimer);
                _coroutineTimer = null;
            }
            _coroutineTimer = StartCoroutine(StartTimerRecipe());
        } else
        {
            _image.sprite = null;
            if (_coroutineTimer != null)
            {
                StopCoroutine(_coroutineTimer);
                _coroutineTimer = null;
            }
        }
        UpdateSliderValueAndColor(1f);
        _hasStartedShaking = false;
        _slider.transform.DOKill();
    }

    IEnumerator StartTimerRecipe()
    {
        while (Client.RemainingWaitingDuration > 0f)
        {
            float ratio = Client.RemainingWaitingDuration / Client.WaitingDuration;
            if (!_hasStartedShaking && ratio <= _thresholdStartShaking) 
            {
                _hasStartedShaking = true;
                _slider.transform.DOShakePosition(1f, _shakeForcePosition).SetLoops(-1);
                _slider.transform.DOShakeRotation(1f, _shakeForceRotation).SetLoops(-1);
            }
            UpdateSliderValueAndColor(ratio);
            yield return null;
        }
        UpdateSliderValueAndColor(0f);
    }

    void UpdateSliderValueAndColor(float value)
    {
        _slider.value = value;
        _fillAreaImage.color = _gradient.Evaluate(value);
    }
}
