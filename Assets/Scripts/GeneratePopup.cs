using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePopup : MonoBehaviour
{
    private Coroutine inProgress;
    TMPro.TMP_Text text;
    public string helpText;
    public static GeneratePopup Instance;
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
        text = GameObject.Find("Canvas/Help Popup").transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
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
            Debug.Log("Popup already in progress");
            StartCoroutine(DelayPopup());
        }
    }
    IEnumerator DelayPopup()
    {
        Debug.Log("Delaying popup");
        yield return new WaitUntil(() => inProgress == null);
        Debug.Log("Popup delayed");
        inProgress = StartCoroutine(WriteText(helpText));
    }
    IEnumerator WriteText(string helpText)
    {
        foreach (char c in helpText)
        {
            text.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(2f);
        inProgress = null;
        text.text = "";
    }
}
