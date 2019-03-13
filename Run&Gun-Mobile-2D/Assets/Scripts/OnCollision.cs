using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public float speed = 2.5f;
    private Rigidbody2D rb;
    private PlayerMovement playerController;
    private GameController gameController;

    public int coinValue;

    public bool isCoin = false;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isCoin)
        {
            other.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log(other.name);
            gameController.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (Vector2.left * speed);
    }
}
