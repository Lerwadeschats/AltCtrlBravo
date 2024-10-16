using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class TestArduino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(5, PinMode.Input_pullup);
    }

    // Update is called once per frame
    void Update()
    {
       int x= UduinoManager.Instance.digitalRead(5);
        Debug.Log(x);
    }
}
