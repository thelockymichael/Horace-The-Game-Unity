using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private Animator anim;

    public float fireRate;

    private float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = (Input.GetAxisRaw("Horizontal") * runSpeed);
            
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            StartCoroutine(jumpDelay());
           // anim.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch")) //&& Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Debug.Log("Crouching");
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //anim.SetBool("IsSliding", true);
        }
    }


    IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(0.01f);
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
