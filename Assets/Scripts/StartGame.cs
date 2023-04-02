using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public InputField uname;
    public string userName;

    void Start()
    {

        // Retrieve the saved username from PlayerPrefs
        userName = PlayerPrefs.GetString("username");

        // Hide the input field if the saved username is not null or empty
        if (!string.IsNullOrEmpty(userName))
        {
            uname.gameObject.SetActive(false);
        }
    }
    public void HighScores()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


    public void StartGameButton()
    {
        if (uname.gameObject.activeSelf)
        {
            userName = uname.text;

            // if the input field is empty, flash and do nothing
            if (string.IsNullOrEmpty(userName))
            {
                FlashInputField(uname, 0.5f);
                Debug.Log(userName);
            }
            else
            {
                // Save the username
                PlayerPrefs.SetString("username", userName);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        else
        {
            // Save the username
            PlayerPrefs.SetString("username", userName);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // flash animation for the input field if empty
    void FlashInputField(InputField field, float duration)
    {
        StartCoroutine(FlashRoutine(field, duration));
    }

    IEnumerator FlashRoutine(InputField field, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            field.image.enabled = !field.image.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        field.image.enabled = true;
    }
}
