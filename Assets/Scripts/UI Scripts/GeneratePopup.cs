// Tahmidul Islam @tahmidul612
using System.Collections;
using UnityEngine;

public class GeneratePopup : MonoBehaviour
{
    private Coroutine inProgress; // The coroutine for the popup
    TMPro.TMP_Text text; // The text component of the popup
    public string helpText; // The text to be written to the popup
    public static GeneratePopup Instance; // The singleton instance
    private void Awake()
    {
        // Set the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        text = GameObject.Find("HUD Canvas/Help Popup").transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        inProgress = null;
    }

    public void showHelpText(string helpText)
    {
        this.helpText = helpText;
        if (inProgress == null)
        {
            inProgress = StartCoroutine(WriteText(helpText));
        }
        else
        {
            //Debug.Log("Popup already in progress");
            StartCoroutine(DelayPopup());
        }
    }
    IEnumerator DelayPopup()
    {
        //Debug.Log("Delaying popup");
        yield return new WaitUntil(() => inProgress == null);
        //Debug.Log("Popup delayed");
        inProgress = StartCoroutine(WriteText(helpText));
    }

    // Set text to the popup, one character at a time
    // Creates a type writer effect
    IEnumerator WriteText(string helpText)
    {
        foreach (char c in helpText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        inProgress = null;
        text.text = "";
    }
}
