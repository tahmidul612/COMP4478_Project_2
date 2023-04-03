using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public static bool isPaused = false;
    private void Start()
    {
        FullscreenController.fullScreenIcon(Screen.fullScreen);
        // Pause button
        Button pauseButton = transform.Find("Pause").GetComponent<Button>();
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseButton.gameObject.SetActive(true);
            pauseButton.onClick.AddListener(delegate { PauseGame(); });
        }
        else
        {
            pauseButton.gameObject.SetActive(false);
        }
    }
    private void PauseGame()
    {
        Time.timeScale = isPaused ? 1 : 0;
        isPaused = !isPaused;
        // GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
    }
}