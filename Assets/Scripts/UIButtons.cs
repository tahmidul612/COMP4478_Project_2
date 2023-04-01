using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject cornerButtonsPanel;
    private Button fullScreenButton;
    private void Start()
    {
        // Fullscreen button
        fullScreenButton = cornerButtonsPanel.transform.Find("Fullscreen").GetComponent<Button>();
        fullScreenIcon(Screen.fullScreen);
        fullScreenButton.onClick.AddListener(delegate { SetFullScreen(); });
        // Pause button
        Button pauseButton = cornerButtonsPanel.transform.Find("Pause").GetComponent<Button>();
        if (SceneManager.GetActiveScene().name == "MainGame") {
            // TODO: add pause functionality
        }
        else {
            pauseButton.gameObject.SetActive(false);
        }
    }
    private void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreenIcon(Screen.fullScreen);
    }
    private void fullScreenIcon(bool value)
    {
        // larger
        fullScreenButton.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        fullScreenButton.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}