// Tahmidul Islam @tahmidul612
using System.Runtime.InteropServices;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Import the closewindow js plugin
    [DllImport("__Internal")]
    private static extern void closeWindow();
    public static void QuitGameButton()
    {
        // Close the window if the game is running on WebGL
        // Otherwise, quit the application
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            closeWindow();
        else
            Application.Quit();
    }
}