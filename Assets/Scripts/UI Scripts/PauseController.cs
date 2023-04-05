using System.Linq;
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
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseButton.onClick.AddListener(delegate { TogglePlayState(); });
        }
        else
        {
            pauseButton.gameObject.SetActive(false);
        }
    }
    public static void TogglePlayState()
    {
        PauseIcon(!isPaused);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        //Debug.Log("Paused");
        isPaused = Time.timeScale == 0;
    }
    private static void PauseIcon(bool value)
    {
        // larger
        pauseButton.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        pauseButton.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}