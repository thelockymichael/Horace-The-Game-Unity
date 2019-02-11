using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public float speed = 2.5f;
    private Rigidbody2D rb;
    private PlayerMovement playerController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        playerController = gameControllerObject.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("You touched me!");
            //playerController
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (Vector2.left * speed);
    }
}
