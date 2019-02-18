using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement02 : MonoBehaviour
{
    private CharacterController2D controller;
    private Animator anim;
    private Rigidbody2D rb;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
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

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Got Hit: " + gotHit);

        if (gotHit == 1)
        {
            playerXPosition = MoveBack.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), speed);
            StartCoroutine(moveBackDelay());
            
        }

        if (gotHit >= 2)
        {
            playerXPosition = deadPoint.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), speed);
        }

        if (gotHit == 0)
        {
            playerXPosition = originPoint.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(playerXPosition, transform.position.y), 0.03f);
        }

        var InputDevice = InputManager.ActiveDevice;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(InputManager.ActiveDevice.DPadUp)
        {
            jump = true;

            StartCoroutine(jumpDelay());
           // anim.SetBool("IsJumping", true);

        }
        if (InputManager.ActiveDevice.DPadUp && slidingFinished)
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
            Debug.Log("Sliding");
            crouch = true;
        }
        else if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            slidingFinished = true;
            crouch = false;
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
