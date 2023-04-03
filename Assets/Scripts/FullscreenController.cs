using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    public static Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }
    public void ToggleFullScreen()
    {
        bool isFullscreen = Screen.fullScreen;
        Screen.fullScreen = !isFullscreen;
        fullScreenIcon(!isFullscreen);
    }
    public static void fullScreenIcon(bool value)
    {
        // larger
        button.transform.GetChild(0).GetComponent<Image>().enabled = !value;
        // smaller
        button.transform.GetChild(1).GetComponent<Image>().enabled = value;
    }
}
