using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void closeWindow();
    public static void QuitGameButton()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            closeWindow();
        else
            Application.Quit();
    }
}