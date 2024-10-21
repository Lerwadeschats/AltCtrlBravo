using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIRecipe : MonoBehaviour
{
    [SerializeField] Image _image;

    [SerializeField] Slider _slider;
    [SerializeField] Gradient _gradient;
    [SerializeField] Image _fillAreaImage;

    private Client _client;
    private Coroutine _coroutineTimer;

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
    }

    IEnumerator StartTimerRecipe()
    {
        while (Client.RemainingWaitingDuration > 0f)
        {
            UpdateSliderValueAndColor(Client.RemainingWaitingDuration / Client.WaitingDuration);
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
