using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    private GameController gameController;
    public float speed = 2.5f;
    private Rigidbody2D rb;
    public int coinValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log(other.name);
           gameController.AddCoins(coinValue);
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2.left * speed);

    }
}