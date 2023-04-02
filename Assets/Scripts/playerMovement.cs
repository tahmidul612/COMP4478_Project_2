using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public int jump = 3;
    private Vector2 jumpHeight;
    Rigidbody2D rb;
    float lastDirection;

    public float lastXMove;
    public float lastYMove;

    public int numCoin = 0;



    public ParticleSystem myParticleSystem; // Declare the particle system variable
    public Material gift; // The material to apply to the particle system
    public Material snow;
    private ParticleSystemRenderer particleSystemRenderer; // The renderer for the particle system
    public int onoff_value = 3;
    private float updateInterval = 2.0f; // The update interval in seconds
    public string uname;


    // Start is called before the first frame update
    void Start()
    {

        uname = PlayerPrefs.GetString("username");
        // Call MyUpdateFunction every updateInterval seconds, starting from 0 seconds
        InvokeRepeating("MyUpdateFunction", 0.0f, updateInterval);


        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        if (xMove != 0)
        {
            lastXMove = xMove;
        }
        if (yMove != 0)
        {
            lastYMove = yMove;
        }

        Vector2 force = new Vector2(xMove, 0) * speed;
        rb.AddForce(force);

        Vector3 clampVel = rb.velocity;
        clampVel.x = Mathf.Clamp(clampVel.x, -10f, 10f);
        rb.velocity = clampVel;

        Vector3 clampVelDown = rb.velocity;
        clampVelDown.y = Mathf.Clamp(clampVel.y, -10f, 10f);
        rb.velocity = clampVelDown;
    }

    void OnCollisionStay2D(Collision2D coll)
    {

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            if (coll.gameObject.tag == "Ground" || coll.gameObject.name == "Tilemap")
            {
                playerJump(coll, 0, jump);
            }
            if (coll.gameObject.tag == "Wall")
            {
                Debug.Log(lastXMove * -1 * (speed / 2));
                playerJump(coll, lastXMove * -1 * (speed / 2), jump);
            }
        }



        if (coll.gameObject.tag == "Door")
        {

            SceneManager.LoadScene("NextScene");


            addScore(uname, 113);
        }
    }

    void playerJump(Collision2D coll, float x, float yMod)
    {
        float height = yMod;
        if (coll.gameObject.GetComponent<customTags>().getBouncy())
        {
            height = (float)(yMod * 1.5);
        }
        jumpHeight = new Vector2(x, height);
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        rb.AddForce(jumpHeight * yMove, ForceMode2D.Impulse);
    }








    // Update is called once per frame
    void MyUpdateFunction()
    {


        //instructor change game on off
        StartCoroutine(FetchData());

        Debug.Log(onoff_value);
    }

    //get the data from control table
    IEnumerator FetchData()
    {
        // Replace "your_php_script.php" with the filename of your PHP script
        UnityWebRequest www = UnityWebRequest.Get("https://advgamin.000webhostapp.com/control.php");

        // Disable SSL verification
        www.certificateHandler = new AcceptAllCertificates();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error fetching data from database: " + www.error);
        }
        else
        {
            onoff_value = int.Parse(www.downloadHandler.text);
            Debug.Log("Value of onoff column: " + onoff_value);
        }

        if (onoff_value == 0)
        {

            // Get the renderer for the particle system
            particleSystemRenderer = myParticleSystem.GetComponent<ParticleSystemRenderer>();

            // Set the new material
            particleSystemRenderer.material = snow;
        }

        else if (onoff_value == 1)
        {
            // Get the renderer for the particle system
            particleSystemRenderer = myParticleSystem.GetComponent<ParticleSystemRenderer>();

            // Set the new material
            particleSystemRenderer.material = gift;
        }
    }



    // send the name and score to scoremanager script to upload it to php file as form
    public void addScore(string name, int score)
    {
        // Create a new ScoreManager object in the scene
        GameObject scoreManagerObj = new("ScoreManager");
        ScoreManager scoreManager = scoreManagerObj.AddComponent<ScoreManager>();

        // Set the player name and score
        scoreManager.name = name;
        scoreManager.score = score;

        // Add the score to the database
        scoreManager.AddScoreToDatabase();
    }
}
