using System;
using UnityEngine;
using UnityEngine.UI;


public class Chronometer : MonoBehaviour
{
    public Text timerDisplay;
    float minutes, seconds;
    private float timer = 0.0f;
    private bool isTimer = false;



    private void Update() {
        
        if(isTimer){
            timer += Time.deltaTime;
            DisplayTime();
        }
    }



    private void DisplayTime() {
        minutes = Mathf.FloorToInt(timer/60.0f);
        seconds = Mathf.FloorToInt(timer % 60);
        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StartTimer() {
        isTimer = true;
    }
    public void StopTimer() {
        isTimer = false;
    }
    public void ResetTimer() {
        timer = 0.0f;
        isTimer = true;
    }
}
