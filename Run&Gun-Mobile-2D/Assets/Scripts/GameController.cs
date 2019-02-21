using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }


    public GameObject[] hazards;
    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;


    public Text coinCountText;
    public Text feetCountText;

    public Text scoreText;
    public Text restartText;
    public Text newHighScoreText;
    public Text highScoreText;
    public Text gameOverText;

    public bool gameOver;
    private bool restart;
    public bool playGame = false;

    private int score;

    public bool running = true;

    public static GameController datamanagement;

    public int highScore = 0;
    public int coinsCollected = 0;

    // Record the player's distance
    public float distance = 1f;

    public GameObject[] backGroundLayers;

    public int playerHasRun;
    void Start()
    {
        highScoreText.text = /*"Hiscore: " + */PlayerPrefs.GetInt("HighScore", 0).ToString();

        newHighScoreText.text = "";

        playerHasRun = 0;
        UpdateScore();

        running = true;
        coinCountText.text = "0";
        feetCountText.text = "0";

        gameOver = false;
        /*
        gameOver = false;
        restart = false;
        restartText.text = "";
        newHighScoreText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();*/
        StartCoroutine(SpawnWaves());
       // highScoreText.text = "Hiscore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        if (playGame)
        {
            if (running)
            {
                ++distance;
                float distanceRun = distance * Time.time;

                Debug.Log("The player has run " + Mathf.RoundToInt(distanceRun) / 200 + " meters.");

                playerHasRun = Mathf.RoundToInt(distanceRun) / 200;
                feetCountText.text = playerHasRun.ToString();

            }
        }
    }
  

    public void AddCoins(int newCoinValue)
    {
        coinsCollected += newCoinValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        coinCountText.text = coinsCollected.ToString();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true && playGame)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
                // bool flag = Random.value();
                // if (flag)
                //{
                //
                //}
                Vector2 spawnPosition = new Vector2(spawnValues.x, spawnValues.y);
                Quaternion spawnRotation = Quaternion.identity;
               GameObject clone = Instantiate(hazard, spawnPosition, spawnRotation);
              //  ReverseDirection(clone);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                foreach (GameObject layer in backGroundLayers)
                {
                    layer.gameObject.GetComponent<BGScroller>().enabled = false;
                }

                feetCountText.text = playerHasRun.ToString();


                if (playerHasRun > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", playerHasRun);
                    highScoreText.text = /*"Hiscore : " + */playerHasRun.ToString();
                    newHighScoreText.text = "New High \nScore!";
                }
                // backGroundLayers.BGScroller();
                restartText.text = "try again";
                restart = true;
                break;
            }
        }
    }
    [Serializable]
    class gameData
    {
        public int highScore;
        public int coinsCollected;
    }

    /*
    void ReverseDirection(GameObject clone)
    {
        //Quaternion rot = transform.localRotation;
        //rot.eulerAngles = new Vector3(0.0f, curY, 0.0f);
        //transform.localRotation = rot;
        clone.transform.rotation.y = 0;
        clone.GetComponent<Mover>().speed = 0;
    }*/

    /*
public void AddScore(int newScoreValue)
{
    score += newScoreValue;
    UpdateScore();
}

void UpdateScore()
{
    scoreText.text = "Score: " + score.ToString();
}

public void GameOver()
{
    gameOverText.text = "game over";
    gameOver = true;
    scoreText.text = "Score: " + score.ToString();

    if(score > PlayerPrefs.GetInt("HighScore", 0))
    {
        PlayerPrefs.SetInt("HighScore", score);
        highScoreText.text = "Hiscore : " + score.ToString();
        newHighScoreText.text = "New High \nScore!";
    }
}*/
}