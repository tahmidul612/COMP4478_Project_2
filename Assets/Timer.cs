using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private TMPro.TMP_Text timerText;
    float currentTime;
    bool timerActive = false;

    void Start()
    {
        timerText = GetComponent<TMPro.TMP_Text>();
        currentTime = 0;
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = "Time: " + time.Seconds.ToString() + " sec";
    }
    public void StartTimer()
    {
        timerActive = true;
    }
    public void StopTimer()
    {
        timerActive = false;
    }
}
