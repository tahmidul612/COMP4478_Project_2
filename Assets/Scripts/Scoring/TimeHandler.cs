using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeHandler : MonoBehaviour
{
    private TMPro.TMP_Text timerText;
    float currentTime;
    public static int timeInSeconds;
    bool timerActive = false;

    void Start()
    {
        timerText = GetComponent<TMPro.TMP_Text>();
        currentTime = 0;
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        int timeInSeconds = TimeSpan.FromSeconds(currentTime).Seconds;
        timerText.text = "Time: " + timeInSeconds + " sec";
    }
    public void StartTimer()
    {
        timerActive = true;
    }
    public void StopTimer()
    {
        timerActive = false;
    }

    public static int GetTimeInSeconds()
    {
        return timeInSeconds;
    }
}
