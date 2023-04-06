// Tahmidul Islam @tahmidul612
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// A rewrite of the GeneratePopup script
// This script is attached to the Game Over text
public class GameOverDisplayScore : MonoBehaviour
{
    private TMPro.TMP_Text TextBox;
    private readonly string gameover = "Game Over";
    private string score;
    private Coroutine inProgress;
    string[] textSwitcher;
    void Start()
    {
        int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        TextBox = GetComponent<TMPro.TMP_Text>();
        TextBox.text = "";
        int TotalScore = 0;

        for (int i = 1; i <= CurrentLevel; i++)
        {
            TotalScore += PlayerPrefs.GetInt("Score" + i);
        }
        score = string.Format("You have scored {0} points!", TotalScore);
        textSwitcher = new string[] { gameover, score };
        StartCoroutine(DelayPopup());

    }

    // Switch between "Game OVer" and "You have scored {0} points!"
    // infinite loop
    IEnumerator DelayPopup()
    {
        for (int i = 0; i <= 1; i = 1 - i)
        {
            inProgress = StartCoroutine(WriteText(textSwitcher[i]));
            yield return new WaitUntil(() => inProgress == null);
        }
    }
    IEnumerator WriteText(string text)
    {
        foreach (char c in text)
        {
            TextBox.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        inProgress = null;
        TextBox.text = "";
    }
}
