using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class High_score : MonoBehaviour
{


    public Text m_Score1;
    public Text m_Score2;
    public Text m_Score3;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button QuitButton;

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton.onClick.AddListener(delegate { MainMenu(); });
        QuitButton.onClick.AddListener(delegate { Application.Quit(); });
        // Create a web request to retrieve the JSON data
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://advgamin.000webhostapp.com/ASSIGN2/highscores.php");
        request.ContentType = "application/json";
        request.Method = "GET";

        // Get the response from the web request
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string json_data = reader.ReadToEnd();

        // Close the streams and response
        reader.Close();
        dataStream.Close();
        response.Close();

        // Parse the JSON data and extract the top scores
        List<Dictionary<string, object>> top_scores = new List<Dictionary<string, object>>();
        List<object> scores_list = MiniJSON.Json.Deserialize(json_data) as List<object>;
        foreach (object score in scores_list)
        {
            Dictionary<string, object> score_dict = score as Dictionary<string, object>;
            top_scores.Add(score_dict);
        }

        // Display the top scores in the console
        foreach (Dictionary<string, object> score in top_scores)
        {
            string fname = score["name"] as string;
            int total_score = Convert.ToInt32(score["score"]);
            int index = top_scores.IndexOf(score);

            if (index == 0)
            {
                m_Score1.text = "1- " + score["name"] + ": " + score["score"];
            }
            else if (index == 1)
            {
                m_Score2.text = "2- " + score["name"] + ": " + score["score"];
            }
            else if (index == 2)
            {
                m_Score3.text = "3- " + score["name"] + ": " + score["score"];
            }
        }
    }
}