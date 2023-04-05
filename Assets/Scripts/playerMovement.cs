using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
    public int onoff_value = 0;
    private float updateInterval = 3.0f; // The update interval in seconds
    public string uname;
    // public int CurrentScore = 0;
    private bool ScoreUpdated;

    // Start is called before the first frame update
    void Start()
    {
        uname = PlayerPrefs.GetString("username");
        // Call MyUpdateFunction every updateInterval seconds, starting from 0 seconds
        InvokeRepeating("MyUpdateFunction", 0.0f, updateInterval);

        rb = GetComponent<Rigidbody2D>();
        ScoreUpdated = false;
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
                //Debug.Log(lastXMove * -1 * (speed / 2));
                playerJump(coll, lastXMove * -1 * (speed / 2), jump);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "NextLevelDoor")
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                // int level = SceneManager.GetActiveScene().buildIndex;

                // if (level == 1 && !scoreUpdated)
                // {
                //     TimeHandler.StopTimer();
                //     float timeTaken = TimeHandler.GetTimeInSeconds();
                //     int CollectedCoins = CoinHandler.GetCoins();
                //     CurrentScore = Mathf.RoundToInt(CollectedCoins * 100 / timeTaken * 10);
                //     PlayerPrefs.SetInt("Score1", CurrentScore);

                //     Debug.Log("level " + level + "  collected money " + CollectedCoins + " time : " + timeTaken + " score " + CurrentScore);
                //     scoreUpdated = true;

                // }
                // else if (level == 2 && !scoreUpdated)
                // {
                //     TimeHandler.StopTimer();
                //     float timeTaken = TimeHandler.GetTimeInSeconds();
                //     int CollectedCoins = CoinHandler.GetCoins();
                //     int PreviousScore = PlayerPrefs.GetInt("Score1");
                //     CurrentScore = Mathf.RoundToInt(CollectedCoins * 100 / timeTaken * 20);
                //     PlayerPrefs.SetInt("Score2", CurrentScore);
                //     Debug.Log("level " + level + "  collected money " + CollectedCoins + " time : " + timeTaken + " score " + CurrentScore);
                //     Debug.Log(string.Format("Level: {0}, CoinCollected: {1} , TimeTaken: {2}, LevelScore: {3}, TotalScore: {4}", level, CollectedCoins, timeTaken, CurrentScore));
                //     addScore(uname, CurrentScore + PreviousScore);
                //     scoreUpdated = true;
                // }
                if (!ScoreUpdated)
                {
                    TimeHandler.StopTimer();
                    float timeTaken = TimeHandler.GetTimeInSeconds();
                    int CollectedCoins = CoinHandler.GetCoins();
                    int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
                    int CurrentScore = Mathf.RoundToInt(CollectedCoins * 100 / timeTaken * 10 * CurrentLevel);
                    PlayerPrefs.SetInt("Score" + CurrentLevel, CurrentScore);
                    int TotalScore = 0;
                    for (int i = 1; i <= CurrentLevel; i++)
                    {
                        TotalScore += PlayerPrefs.GetInt("Score" + i);
                    }
                    Debug.Log(string.Format("Level: {0}, CoinCollected: {1} , TimeTaken: {2}, LevelScore: {3}, TotalScore: {4}", CurrentLevel, CollectedCoins, timeTaken, CurrentScore, TotalScore));
                    if (CurrentLevel == SceneManager.sceneCountInBuildSettings - 3)
                    {
                        addScore(uname, TotalScore);
                    }
                    ScoreUpdated = true;
                }
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
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

        //Debug.Log(onoff_value);
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
            //Debug.Log("Error fetching data from database: " + www.error);
        }
        else
        {
            onoff_value = int.Parse(www.downloadHandler.text);
            //Debug.Log("Value of onoff column: " + onoff_value);
        }

        if (onoff_value == 0)
        {

            // Stop the particle system
            myParticleSystem.Stop();

        }

        else if (onoff_value == 1)
        {
            myParticleSystem.Play();


            // Get the renderer for the particle system
            particleSystemRenderer = myParticleSystem.GetComponent<ParticleSystemRenderer>();

            // Set the new material
            particleSystemRenderer.material = gift;

        }

        else if (onoff_value == 2)
        {
            myParticleSystem.Play();
            // Get the renderer for the particle system
            particleSystemRenderer = myParticleSystem.GetComponent<ParticleSystemRenderer>();

            // Set the new material
            particleSystemRenderer.material = snow;
        }


    }

    // send the name and score to scoremanager script to upload it to php file as form
    public void addScore(string name, int score)
    {
        // Create a new ScoreManager object in the scene
        GameObject scoreManagerObj = new GameObject("ScoreManager");
        ScoreManager scoreManager = scoreManagerObj.AddComponent<ScoreManager>();

        // Set the player name and score
        scoreManager.name = name;
        scoreManager.score = score;

        // Add the score to the database
        scoreManager.AddScoreToDatabase();
    }




















}
