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

        var InputDevice = InputManager.ActiveDevice;

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

