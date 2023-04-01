using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject cornerButtonsPanel;
    private void Start()
    {
        // Fullscreen button
        Button fullScreenButton = cornerButtonsPanel.transform.Find("Fullscreen").GetComponent<Button>();
        fullScreenIcon(fullScreenButton, Screen.fullScreen);
        fullScreenButton.onClick.AddListener(delegate { SetFullScreen(fullScreenButton, Screen.fullScreen); });
        // Pause button
        Button pauseButton = cornerButtonsPanel.transform.Find("Pause").GetComponent<Button>();
        if (SceneManager.GetActiveScene().name == "MainGame") {
            // TODO: add pause functionality
        }
        else {
            pauseButton.gameObject.SetActive(false);
        }
    }
    private void SetFullScreen(Button button, bool isFullscreen)
    {
        Screen.fullScreen = !isFullscreen;
        fullScreenIcon(button, !isFullscreen);
    }
    private void fullScreenIcon(Button button, bool value)
    {
        // larger
        button.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        button.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}