// Tahmidul Islam @tahmidul612
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadSceneAsync("StartMenu"); });
    }
}