using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipe : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float _shakeForcePosition = 1f;
    [SerializeField] float _shakeForceRotation = 15f;

    [SerializeField] Slider _slider;
    [SerializeField] Gradient _gradient;
    [SerializeField] Image _fillAreaImage;

    [Header("Animation")]
    [SerializeField] bool _activateAnimation = true;
    [SerializeField] float _timeSlideTransition = 0.5f;
    [SerializeField] float _shakeForceSlideTransition = 0.5f;
    [SerializeField] Vector3 _offsetPosition;
    Vector3 _originalPosition;
    Coroutine _coroutineSlide;


    private Client _client;
    private Coroutine _coroutineTimer;
    private bool _hasStartedShaking = false;
    private float _thresholdStartShaking = 0.2f;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    public Client Client {
        get => _client;
        set {
            
            if (value != _client && 
                (value == null || value.IsWaiting))
            {
                Client oldClient = _client;
                _client = value;
                UpdateUIRecipe(oldClient);
            }
        }
    }

    private void UpdateUIRecipe(Client oldClient)
    {
        if (Client != null)
        {
            _image.sprite = Client.Recipe.UIRecipeSprite;
            if ((oldClient == null || !oldClient.IsWaiting) && _activateAnimation)
            {
                if (_coroutineSlide == null)
                {
                    _coroutineSlide = StartCoroutine(SlideCommand());
                }
            } 

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

    IEnumerator SlideCommand()
    {
        float timer = 0f;
        Vector3 startPosition = _originalPosition + _offsetPosition;
        Vector3 endPosition = _originalPosition;

        transform.DOShakePosition(1f, _shakeForceSlideTransition).SetLoops(-1);
        while (timer < _timeSlideTransition)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / _timeSlideTransition);
            yield return null;
        }
        transform.position = endPosition;
        transform.DOKill();
    }
}
