using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScript : MonoBehaviour
{

    public Transform deadPoint;
    public Transform MoveBack;  // Using it to grab the player's position
    public Transform originPoint;

    public float speed = 0.025f;

    private float bearXPosition;

    private Animator anim;

    private PlayerMovement02 playerController;

    private bool attackTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerMovement02>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerController.gotHit == 0 && !playerController.gameOver)
        {
            attackTrigger = false;
            //anim.SetBool("bearAttack", false);
            bearXPosition = originPoint.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(bearXPosition, transform.position.y), 0.03f);
        }

        if (playerController.gotHit == 1 && !playerController.gameOver)
        {
            if(!attackTrigger)
            {
                anim.SetTrigger("bearAttack");
                attackTrigger = true;
            }
            bearXPosition = MoveBack.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(bearXPosition, transform.position.y), 0.03f);
        }

        if (playerController.gotHit >= 2)
        {
            //anim.SetBool("bearAttack", false);
            bearXPosition = deadPoint.position.x;
            transform.position = Vector2.Lerp(transform.position, new Vector2(bearXPosition, transform.position.y), 0.03f);
        }

        bearXPosition = originPoint.position.x;
        transform.position = Vector2.Lerp(transform.position, new Vector2(bearXPosition, transform.position.y), 0.03f);
    }
}
