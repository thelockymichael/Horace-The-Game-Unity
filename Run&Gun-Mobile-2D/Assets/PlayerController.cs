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
        public CharacterController2D playerController;

    private float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;

    private Rigidbody2D rb;
        public float speed = 5f;
        public Boundary boundary;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        playerController.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;

        var InputDevice = InputManager.ActiveDevice;

        horizontalMove = InputDevice.LeftStickX * runSpeed;


        if (InputDevice.DPadLeft.IsPressed)
        {

        }
        if (InputDevice.DPadRight.IsPressed)
        {
            //Vector2 movement = new Vector2(moveHorizontal, 0);
            //InputDevice.DPadRight.IsPressed
        }

        // float moveLeft = Vector2 movement = new Vector2(moveHorizontal, 0);
        //float moveVertical = InputDevice.LeftStickY;


        // float moveHorizontal = Input.GetAxis("Horizontal");
        //  float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector2(horizontalMove, 0.0f);

        rb.velocity = movement * speed;

            rb.position = new Vector2(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
                );
            //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }

