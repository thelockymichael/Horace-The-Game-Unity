using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, yMin, yMax;
    }


    private CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool slidingFinished = false;

    private Animator anim;
    private Rigidbody2D rb;

    public float fireRate;

    private float nextFire;

    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

// Update is called once per frame
void Update()
    {

        var InputDevice = InputManager.ActiveDevice;


        // horizontalMove = (InputManager.ActiveDevice.LeftStickX * runSpeed);
        // horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;

        //rb.velocity = new Vector2(horizontalMove * 10, 0);
        /*
        if (InputManager.ActiveDevice.DPadLeft.IsPressed)
        {
            rb.velocity = (Vector2.left * runSpeed);
        }
        if (InputManager.ActiveDevice.DPadRight.IsPressed)
        {
            rb.velocity = (Vector2.left * -runSpeed);
        }*/

        if (InputManager.ActiveDevice.DPadUp && slidingFinished)
        {
            slidingFinished = false;
            jump = true;
            StartCoroutine(jumpDelay());
            // anim.SetBool("IsJumping", true);
        }
        if (InputManager.ActiveDevice.DPadDown) //&& Time.time > nextFire)
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

        Vector2 movement = new Vector2(horizontalMove, 0.0f);
      // rb.velocity = movement * runSpeed;

        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
            );
    }

    IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(0.05f);
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("gotHurt");

        }
    }

}

