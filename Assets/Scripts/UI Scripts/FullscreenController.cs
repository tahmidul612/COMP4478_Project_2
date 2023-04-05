using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    private Button button;
    private bool isFullScreen;
    void Start()
    {
        button = GetComponent<Button>();
        isFullScreen = Screen.fullScreen;
        FullScreenIcon(isFullScreen);
    }
    private void ToggleFullScreen()
    {
        isFullScreen = !Screen.fullScreen;
        Screen.fullScreen = isFullScreen;
        FullScreenIcon(isFullScreen);
        // GameObject settings = GameObject.Find("Settings");
        // if (settings != null && settings.activeSelf)
        // {
        //     SettingsController.SetupResolutions();
        // }
    }
    private void FullScreenIcon(bool value)
    {
        // larger
        button.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        button.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}
