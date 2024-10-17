using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipe : MonoBehaviour
{
    [SerializeField] Image _image;

    [SerializeField] Slider _slider;
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
            _slider.value = 1f;
        }
    }

    IEnumerator StartTimerRecipe()
    {
        while (Client.CurrentWaitingDuration > 0f)
        {
            _slider.value = Client.CurrentWaitingDuration / Client.WaitingDuration;
            yield return null;
        }
    }
}
