using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public static bool isPaused = false;
    private Button pauseButton;
    void Start()
    {
        FullscreenController.fullScreenIcon(Screen.fullScreen);
        pauseButton = transform.GetComponentsInChildren<Button>(true).Where<Button>(b => b.name == "Pause").FirstOrDefault();
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseButton.gameObject.SetActive(true);
            pauseButton.onClick.AddListener(delegate { TogglePlayState(); });
        }
        else
        {
            pauseButton.gameObject.SetActive(false);
        }
    }
    private void TogglePlayState()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        isPaused = Time.timeScale == 0;
        // GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
    }
}