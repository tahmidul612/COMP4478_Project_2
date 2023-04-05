using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public int jump;
    private Vector2 jumpHeight;
    Rigidbody2D rb;
    float lastDirection;
    public float maxSpeed = 10f;

    public float lastXMove;
    public float lastYMove;

    AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip jumpClip;
    public AudioClip stickySlimeClip;
    public AudioClip bouncySlimeClip;
    public AudioClip slideySlimeClip;

    private bool isMoving;
    //
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
        jump = 5;
        audioSource = gameObject.GetComponent<AudioSource>();
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

        if (xMove != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            if (audioSource.clip == walkClip)
            {
                stopAudio();
            }
        }

        Vector2 force = new Vector2(xMove, 0) * speed;
        rb.AddForce(force, ForceMode2D.Impulse);

        Vector3 clampVel = rb.velocity;
        clampVel.x = Mathf.Clamp(clampVel.x, -maxSpeed, maxSpeed);
        rb.velocity = clampVel;

    }

    void OnCollisionStay2D(Collision2D coll)
    {

        if (Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Jump") == 1)
        {
            if (coll.gameObject.tag == "Ground")
            {
                playerJump(coll, 0, jump);
                stopAudio();
                playAudio(jumpClip);
            }
            if (coll.gameObject.GetComponent<customTags>().getSticky())
            {
                playerJump(coll, lastXMove * -1 * (speed / 2), jump);
            }
        }
        if (coll.gameObject.GetComponent<customTags>().getSlidey())
        {
            maxSpeed = 20f;
        }
        else
        {
            maxSpeed = 10f;
        }

        if (isMoving && coll.gameObject.tag == "Ground" && !audioSource.isPlaying)
        {
            playAudio(walkClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.GetComponent<customTags>();
        if (tag.getSticky())
        {
            audioSource.PlayOneShot(stickySlimeClip, 0.7f);
        }
        if (tag.getBouncy())
        {
            audioSource.PlayOneShot(bouncySlimeClip, 0.7f);
        }
        if (tag.getSlidey())
        {
            audioSource.PlayOneShot(slideySlimeClip, 0.7f);
        }
    }

    public void playAudio(AudioClip clipTP)
    {
        audioSource.clip = clipTP;
        audioSource.Play();
    }

    public void stopAudio()
    {
        audioSource.Stop();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<customTags>().getSticky())
        {
            lastXMove = lastXMove * -1;
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
        rb.AddForce(jumpHeight, ForceMode2D.Impulse);
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
