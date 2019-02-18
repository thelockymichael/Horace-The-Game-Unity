using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement02 : MonoBehaviour
{
    private CharacterController2D controller;
    private Animator anim;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool slidingFinished = false;

    public float fireRate;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        var InputDevice = InputManager.ActiveDevice;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(InputManager.ActiveDevice.DPadUp/*Input.GetButtonDown("Jump")*/)
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

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}
