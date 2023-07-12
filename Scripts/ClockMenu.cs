using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClockMenu : MonoBehaviour
{
    public TextMeshProUGUI display;

    public string hour;
    public string minute;
    public string second;

    // Update is called once per frame
    void Update()
    {
        Clock();
    }

    void Clock()
    {
        hour = System.DateTime.Now.Hour.ToString();
        minute = System.DateTime.Now.Minute.ToString();
        second = System.DateTime.Now.Second.ToString();
        if (second.Length == 1)
            second = "0" + second;

        display.text = hour + ":" + minute;
    }
}
