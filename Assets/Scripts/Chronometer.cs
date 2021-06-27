using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chronometer : MonoBehaviour
{
    public Text timerDisplay;
    float minutes, seconds;

    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        timerDisplay.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
