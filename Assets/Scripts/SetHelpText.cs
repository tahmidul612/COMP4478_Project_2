using System.Collections;
using UnityEngine;

public class SetHelpText : MonoBehaviour
{
    TMPro.TMP_Text text;
    string storeText;
    private Coroutine writeTextActive;
    private void Start()
    {
        text = GameObject.Find("Canvas/Help Popup").transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Debug.Log(GameObject.Find("Canvas/Help Popup"));
            text.text = "";
            storeText = GenerateHelpText();
            if (writeTextActive == null)
            {
                writeTextActive = StartCoroutine(WriteText(storeText));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(ResetText());
        }
    }
    IEnumerator ResetText()
    {
        yield return new WaitUntil(() => writeTextActive == null);
        text.text = "";
    }
    IEnumerator WriteText(string helpText)
    {
        foreach (char c in helpText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        writeTextActive = null;
    }
    private string GenerateHelpText()
    {
        return this.name switch
        {
            "Slippery" => "Watch out for the slime on the floor! It's slippery",
            "Spike" => "Watch out for the spikes! They're sharp",
            "Sticky" => "You can stick to the walls with the sticky slime",
            "Bouncy" => "You can bounce off the bouncy slime",
            _ => ""
        };
    }
}
