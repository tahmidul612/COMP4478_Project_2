using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    // public InputField playerName;
    new public string name;
    public int score;

    public void AddScoreToDatabase()
    {
        StartCoroutine(AddScore());
    }

    IEnumerator AddScore()
    {
        // Get the URL of the PHP script
        string url = "https://advgamin.000webhostapp.com/ASSIGN2/score.php";

        // Create a form with the name and score parameters
        WWWForm form = new WWWForm();
        //   form.AddField("name", playerName);
        form.AddField("name", name.ToString());
        form.AddField("score", score.ToString());
        // Send a POST request to the PHP script with the form data
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // Disable SSL verification
        request.certificateHandler = new AcceptAllCertificates();

        yield return request.SendWebRequest();

        // Check if there was an error with the request
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
public class AcceptAllCertificates : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}