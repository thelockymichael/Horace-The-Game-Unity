using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

    public class PlayerController : MonoBehaviour
    {

    private float horizontalMove = 0f;
    public float runSpeed = 40f;
    public float jumpForce = 50f;
    public bool grounded;


    private Rigidbody2D rb;
    public float speed = 5f;
    public Boundary boundary;

    private Animator anim;

    public float groundedHeight = 0.5f; // the height above ground to determine if the player is grounded
    public float checkRate = 1.0f; // how often in seconds we check to see if we are grounded
   // public bool grounded = false;// is grounded or not
    public LayerMask groundLayer;//the layer on which we can be grounded
    public float heightOffset = 0.25f; // we dont want to cast from the players feet (may cast underground sometimes), so we offset it a bit

    // Start is called before the first frame update
    void Start()
        {
        InvokeRepeating("GroundCheck", 0, checkRate);
        anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }
    void GroundCheck()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Update()
    {
        anim.SetBool("Grounded", grounded);

    }

    // Update is called once per frame
    void FixedUpdate()
        {

        var InputDevice = InputManager.ActiveDevice;

        if (InputManager.ActiveDevice.DPadUp)
        {
            rb.AddForce(Vector2.up * jumpForce);
          //  anim.SetBool("IsJumping", true);
            Debug.Log("D PAD UP");
        }

        if (InputManager.ActiveDevice.DPadDown)
        {
            Debug.Log("D PAD DOWN");
        }
    
      
        float h = InputManager.ActiveDevice.LeftStickX;
        /*
        if (InputManager.ActiveDevice.DPadRight.IsPressed)
        {
            Debug.Log("PLayer goes right");
            rb.AddForce((Vector2.right * speed) * h);
        }

        if (InputManager.ActiveDevice.DPadLeft.IsPressed)
        {
            Debug.Log("PLayer goes left");
            rb.AddForce((Vector2.right * -speed) * h);
        }*/


        rb.AddForce((Vector2.right * speed) * h);
      
        Vector3 movement = new Vector2(horizontalMove, 0.0f);

        rb.velocity = movement * speed;

            rb.position = new Vector2(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
                );
            //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }

