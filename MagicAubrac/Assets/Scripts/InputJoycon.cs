using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IIMEngine.SFX;

public class InputJoycon : MonoBehaviour
{
    [SerializeField] private float _thresholdShaking = 1f;
    [SerializeField] private float _shakeExtensionDuration = 0.05f;
    private List<Joycon> joycons;
    private Vector3 _gyro;

    private bool _isShaking = false;
    private bool _isInShakeExtension = false;
    private float _timerShake = 0f;
    private float _timerShakeExtension = 0f;
    private Coroutine _coroutineShakeExtension;

    [SerializeField] string clipShake;

    public event Action OnStartShaking;
    public event Action<float> OnStopShaking;

    void Start()
    {
        _gyro = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon joycon = joycons[0];

            // Accel values:  x, y, z axis values (in Gs)
            _gyro = joycon.GetGyro();
            float magnitude = _gyro.magnitude;
            if (magnitude > _thresholdShaking)
            {
                //Started shaking
                if (!_isShaking)
                {
                    _isShaking = true;

                    if (_isInShakeExtension)
                    {
                        if (_coroutineShakeExtension != null)
                        {
                            StopCoroutine(_coroutineShakeExtension);
                            _coroutineShakeExtension = null;
                        }
                        OnStartShaking?.Invoke();
                        _isInShakeExtension = false;
                    } else
                    {
                        SFXsManager.Instance?.PlaySound(clipShake);
                        _timerShake = 0f;

                    }
                }
            }
            else
            {
                //Stopped shaking
                if (_isShaking && !_isInShakeExtension)
                {
                    _isShaking = false;
                    _isInShakeExtension = true;
                    StartRoutineShakeExtension();
                }
            }
        }

        if (_isShaking || _isInShakeExtension)
        {
            _timerShake += Time.deltaTime;
        }
    }

    private void StartRoutineShakeExtension()
    {
        if (_coroutineShakeExtension != null)
        {
            StopCoroutine(_coroutineShakeExtension);
            _coroutineShakeExtension = null;
        }
        _coroutineShakeExtension = StartCoroutine(RoutineShakeExtension());
    }

    IEnumerator RoutineShakeExtension()
    {
        SFXsManager.Instance.StopSound(clipShake);
        _timerShakeExtension = 0f;
        while (_timerShakeExtension < _shakeExtensionDuration)
        {
            _timerShakeExtension += Time.deltaTime; 
            yield return null;
        }
        _isInShakeExtension = false;
        OnStopShaking?.Invoke(_timerShake);
    }

    //private void OnGUI()
    //{
    //    string debug;
    //    if (_isShaking && !_isInShakeExtension)
    //    {
    //        debug = $"SHAKING {_timerShake}";
    //    }
    //    else if (!_isShaking && _isInShakeExtension)
    //    {
    //        debug = $"SHAKING EXTENSION {_timerShake}";
    //    } else if (!_isShaking && !_isInShakeExtension)
    //    {
    //        debug = "NOT SHAKING";
    //    } else
    //    {
    //        debug = "??? ";
    //    }
    //    debug += $" {_gyro}";
    //    GUI.skin.label.fontSize = 30;
    //    GUILayout.Label(debug, GUILayout.Width(300), GUILayout.Height(150));
    //}
}
