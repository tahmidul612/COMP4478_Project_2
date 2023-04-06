// Tahmidul Islam @tahmidul612
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;
    private static Button pauseButton;
    void Start()
    {
        pauseButton = GetComponent<Button>();
        PauseIcon(isPaused);

        // Only show the pause button on the level scenes
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseButton.onClick.AddListener(delegate { TogglePlayState(); });
        }
        else
        {
            pauseButton.gameObject.SetActive(false);
        }
    }

    // Toggle the pause state using timeScale, and pause audio
    public static void TogglePlayState()
    {
        PauseIcon(!isPaused);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        //Debug.Log("Paused");
        isPaused = Time.timeScale == 0;
        AudioListener.pause = isPaused;
    }

    // Change the button icon to show the current state
    private static void PauseIcon(bool value)
    {
        // larger
        pauseButton.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        pauseButton.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}