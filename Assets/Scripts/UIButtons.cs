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
        PauseIcon(isPaused);
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
        PauseIcon(!isPaused);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        isPaused = Time.timeScale == 0;
        // GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
    }
    private void PauseIcon(bool value)
    {
        // larger
        pauseButton.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        pauseButton.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}