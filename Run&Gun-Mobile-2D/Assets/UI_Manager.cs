using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    public GameObject[] backGroundLayers;

    public void TouchToStart()
    {

        gameController.playGame = true;
        foreach (GameObject layer in backGroundLayers)
        {
            layer.gameObject.GetComponent<BGScroller>().startMoving = true;
        }
    }

    public void Restart()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
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
