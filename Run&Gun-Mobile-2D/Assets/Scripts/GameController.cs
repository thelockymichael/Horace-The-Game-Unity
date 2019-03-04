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

    OnCollision[] OnCollisions;

    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;


    public Text coinCountText;
    public Text feetCountText;
    
    public Text newHighScoreText;
    public Text highScoreText;

    public Text currencyBalance;
    public int currentBalance;
    public int currencyInRun = 0;

    public bool gameOver;
    private bool restart;
    public bool playGame = false;

    private int score;

    public bool running = true;

    public static GameController datamanagement;

    public int highScore = 0;
    public int coinsCollected = 0;

    // Record the player's distance
    public float distance = 0f;

    public GameObject[] backGroundLayers;

    public int playerHasRun = 0;
    private int distanceRunDividedBy = 10;
    void Start()
    {

        distance = 0;
        currentBalance = PlayerPrefs.GetInt("currencyPref");

        currencyBalance.text = currentBalance.ToString();

        highScoreText.text = /*"Hiscore: " + */PlayerPrefs.GetInt("HighScore", 0).ToString();

        newHighScoreText.text = "";

        playerHasRun = 0;
        UpdateScore();

        running = true;
        coinCountText.text = "0";
        feetCountText.text = "0";

        gameOver = false;
 
    }

    public void startGame()
    {
        StartCoroutine(SpawnWaves());
        playGame = true;
    }

    private void Update()
    {
        

        if (playGame)
        {
            if (running)
            {
               ++distance;
                float distanceRun = distance  + Time.time;

                //         Debug.Log("The player has run " + Mathf.RoundToInt(distanceRun) / 200 + " meters.");

                playerHasRun = Mathf.RoundToInt(distanceRun);// / distanceRunDividedBy;
                feetCountText.text = playerHasRun.ToString();
            }
        }
    }

    public void AddCoins(int newCoinValue)
    {
        coinsCollected += newCoinValue;
        currencyInRun += newCoinValue;
        UpdateScore();
    }

    void UpdateScore()
    {
      
        coinCountText.text = coinsCollected.ToString();
    }


    public void StopAllEnemies()
    {
        Debug.Log("stopAllEnemies");
        /* int vihu = 0;
         vihu++;
         int enemyAmount = vihu;
         Debug.Log(enemyAmount);*/
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");

        string[] tagsToDisable =
                {
                 "Enemy",
                 "Coin"
             };
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Debug.Log(enemies.Length);
        int i = tagsToDisable.Length;
        foreach (string tag in tagsToDisable)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
            i++;                                    //Increment loop
            foreach (GameObject gameObj in gameObjects)
            {
                gameObj.GetComponent<OnCollision>().speed = 0f;
            }
            //Debug.Log("EMEMIES = " + (i));
            if (i == 0)
            {
                //     Debug.Log("NO EMEMIES LEFT IN SCENE");
            }
  
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
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
                StopAllEnemies();
           

                feetCountText.text = playerHasRun.ToString();

                currentBalance += currencyInRun;
                PlayerPrefs.SetInt("currencyPref", currentBalance);

                currencyBalance.text = currentBalance.ToString();

                currencyInRun = 0; // reset this for the next round

                if (playerHasRun > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", playerHasRun);
                    highScoreText.text = /*"Hiscore : " + */playerHasRun.ToString();
                    newHighScoreText.text = "New High \nScore!";
                }
                // backGroundLayers.BGScroller();
               // restartText.text = "try again";
              //  restart = true;
                break;
            }
        }
    }
}
