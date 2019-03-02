using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;
    public GameObject playButton;

    // Menus
    public GameObject pauseMenu;
    private bool GameIsPaused = false;


    public GameObject[] backGroundLayers;

    public void PauseGame()
    {
    if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void TouchToStart()
    {
        gameController.startGame();
        playButton.SetActive(false);
        foreach (GameObject layer in backGroundLayers)
        {
            layer.gameObject.GetComponent<BGScroller>().startMoving = true;
        }
    }

    public void Restart()
    {
         Time.timeScale = 1.0f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Menus
        pauseMenu.SetActive(false);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
