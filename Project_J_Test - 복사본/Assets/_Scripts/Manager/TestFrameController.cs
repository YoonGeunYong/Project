using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFrameController : MonoBehaviour
{
    public int frame;

    void Start()
    {
        frame = Application.targetFrameRate;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
            Application.targetFrameRate = 30;

        else if (Input.GetKeyDown(KeyCode.Keypad2))
            Application.targetFrameRate = 60;

        else if (Input.GetKeyDown(KeyCode.Keypad3))
            Application.targetFrameRate = 120;

        else if (Input.GetKeyDown(KeyCode.Keypad4))
            Application.targetFrameRate = 144;

        frame = Application.targetFrameRate;
    }
}
