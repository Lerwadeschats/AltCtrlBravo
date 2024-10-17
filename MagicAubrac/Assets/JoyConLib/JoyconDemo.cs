using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour {
	
    [SerializeField] private float _thresholdAccel = 1f;
    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    private Vector3 _accel;
    public int jc_ind = 0;

    private bool _isShaking = false;
    private bool _startShaking = false;
    private float _timerShake = 0f;

    void Start ()
    {
        _accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
	}

    // Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];

            // Accel values:  x, y, z axis values (in Gs)
            _accel = j.GetAccel();

            float magnitude = _accel.magnitude;
            if (magnitude > _thresholdAccel)
            {
                if (!_startShaking)
                {
                    _timerShake = 0f;
                    _startShaking = true;
                    _isShaking = true;
                }
            } else
            {
                if (_startShaking)
                {
                    _startShaking = false;
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
            debug = "SHAKING "+_timerShake;
        } else
        {
            debug = "NOT SHAKING";
        }
        GUI.skin.label.fontSize = 30;
        GUILayout.Label(debug, GUILayout.Width(300), GUILayout.Height(50));
    }
}