using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour {
	
    [SerializeField] private float _thresholdShaking = 1f;
    private List<Joycon> joycons;

    private Vector3 _gyro;

    private bool _isShaking = false;
    private float _timerShake = 0f;
    private float _magnitude;

    public event Action OnStartShaking;
    public event Action<float> OnStopShaking;

    void Start ()
    {
        _gyro = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
	}

    // Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon joycon = joycons [0];

            // Accel values:  x, y, z axis values (in Gs)
            _gyro = joycon.GetGyro();
            _magnitude = _gyro.magnitude;
            if (_magnitude > _thresholdShaking)
            {
                //Started shaking
                if (!_isShaking)
                {
                    OnStartShaking?.Invoke();
                    _timerShake = 0f;
                    _isShaking = true;
                }
            } else
            {
                //Stopped shaking
                if (_isShaking)
                {
                    OnStopShaking?.Invoke(_timerShake);
                    _isShaking = false;
                }
            }
        }

        if (_isShaking)
        {
            _timerShake += Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        string debug;
        if (_isShaking)
        {
            debug = "SHAKING " + _timerShake;
        }
        else
        {
            debug = "NOT SHAKING";
        }
        debug += $" {_gyro} {_magnitude}";
        GUI.skin.label.fontSize = 30;
        GUILayout.Label(debug, GUILayout.Width(300), GUILayout.Height(150));
    }
}