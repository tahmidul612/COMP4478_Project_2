// Tahmidul Islam @tahmidul612
using UnityEngine;
using UnityEngine.SceneManagement;

// Start timer when the level is loaded
// Contains static methods to get the current time,
// and to start and stop the timer
public class TimeHandler : MonoBehaviour
{
    private TMPro.TMP_Text timerText;
    private static float currentTime;
    private static bool timerActive = false;

    void Start()
    {
        timerText = GetComponent<TMPro.TMP_Text>();
        currentTime = 0f;
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
            timerText.text = "Time: " + Mathf.RoundToInt(currentTime) + " sec";
        }
    }
    public static void StartTimer()
    {
        timerActive = true;
    }
    public static void StopTimer()
    {
        timerActive = false;
    }

    public static float GetTimeInSeconds()
    {
        return currentTime;
    }
}
