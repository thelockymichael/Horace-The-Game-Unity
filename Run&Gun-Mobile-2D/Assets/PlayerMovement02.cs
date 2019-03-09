using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement02 : MonoBehaviour
{
    private CharacterController2D controller;
    private GameController gameController;

    private Animator anim;
    private Rigidbody2D rb;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private bool jumpingAllowed = true;
    bool crouch = false;
    bool slidingFinished = false;

    public float fireRate;
    private float nextFire;

    private bool hit = true;
    public bool moveBack = false;
    public float moveBackTime = 2f;

    public int gotHit;


    public Transform deadPoint; 
    public Transform MoveBack;  // Using it to grab the player's position
    public Transform originPoint; 

    public float speed = 0.025f;

    private float playerXPosition;

    public bool gameOver = false;

    // Reference Game Objects


    public float timeLimit;
    private bool timeOut;
    private float timer;
    private float innerTimer;
    private bool executeOnce = false;

    public GameObject[] backGroundLayers;

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        gotHit = 0;
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator moveBackDelay()
    {
        yield return new WaitForSeconds(moveBackTime);
        gotHit = 0;
    }
    // Update is called once per frame



    void Update()
    {
        anim.SetBool("isRunning", false);

        if (gameController.playGame)
        {
            void SpeedUp()
            {
                foreach (GameObject layer in backGroundLayers)
                {
                    //      layer.gameObject.GetComponent<BGScroller>().speed = 0.4f;
                }
            }


            void SlowDown()
            {
                foreach (GameObject layer in backGroundLayers)
                {
                    //     layer.gameObject.GetComponent<BGScroller>().speed = 0.2f;
                }
            }

            if (gotHit == 1 && !gameOver)
            {
                SlowDown();
                timer += Time.deltaTime;
                // innerTimer -= Time.deltaTime;
                //Debug.Log(Mathf.RoundToInt(timer));
                playerXPosition = MoveBack.position.x;
                transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), speed);
                // StartCoroutine(moveBackDelay());

            }
            if (timer > timeLimit)
            {
                executeOnce = true;
                gotHit = 0;
                timer = 0;
            }

            if (gotHit >= 2)
            {
                jumpingAllowed = false;
                gameController.running = false;
                anim.SetBool("gameOver", true);
                gameController.gameOver = true;
                gameOver = true;
                playerXPosition = deadPoint.position.x;
                transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), speed);
            }

            if (gotHit == 0 && !gameOver)
            {
                playerXPosition = originPoint.position.x;
                transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), 0.03f);
            }
            if (executeOnce)
            {
                executeOnce = false;
                SpeedUp();
            }


            var InputDevice = InputManager.ActiveDevice;

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            anim.SetBool("isRunning", true);

            if (InputManager.ActiveDevice.DPadUp && jumpingAllowed)
            {
                jump = true;

                StartCoroutine(jumpDelay());
                // anim.SetBool("IsJumping", true);

            }
            if (InputManager.ActiveDevice.DPadUp && slidingFinished && jumpingAllowed)
            {
                slidingFinished = false;
                jump = true;
                StartCoroutine(jumpDelay());
                //anim.SetBool("IsJumping", true);
            }
            if (InputManager.ActiveDevice.DPadDown)
            {
                nextFire = Time.time + fireRate;
                slidingFinished = false;
                //Debug.Log("Sliding");
                crouch = true;
            }
            else if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                slidingFinished = true;
                crouch = false;
            }
        }
    }
         
    IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("IsJumping", true);
    }

    IEnumerator slideDelay()
    {
        yield return new WaitForSeconds(1.23f);
        crouch = false;
    }

    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        anim.SetBool("IsSliding", isCrouching);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && hit)
        {
            hit = false;
            gotHit++;
            anim.SetTrigger("gotHurt");
           // moveBack = true;
            StartCoroutine(getHitAgainDelay());
        }
    }

    IEnumerator getHitAgainDelay()
    {
        yield return new WaitForSeconds(0.6f);
        hit = true;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;   
    }
}
