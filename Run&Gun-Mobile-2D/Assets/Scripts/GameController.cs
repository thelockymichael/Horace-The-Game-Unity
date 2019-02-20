using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Coin system
[System.Serializable]
public class scoreSystem
{
    public int coins;

}

public class GameController : MonoBehaviour
{


    public scoreSystem scoreSystem;

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
    private int score;

    public bool running = true;



    // Record the player's distance
    public float distance = 1f;

    public GameObject[] backGroundLayers;

    void Start()
    {
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
        if (running)
        {
            ++distance;
            float distanceRun = distance * Time.time;

            Debug.Log("The player has run " + Mathf.RoundToInt(distanceRun) / 200 + " meters.");

            int playerHasRun = Mathf.RoundToInt(distanceRun) / 200;
            feetCountText.text = playerHasRun.ToString();

        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }

    public void AddCoins(int newCoinValue)
    {
        scoreSystem.coins += newCoinValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        coinCountText.text = scoreSystem.coins.ToString();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
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
               // backGroundLayers.BGScroller();
                restartText.text = "try again";
                restart = true;
                break;
            }
        }
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