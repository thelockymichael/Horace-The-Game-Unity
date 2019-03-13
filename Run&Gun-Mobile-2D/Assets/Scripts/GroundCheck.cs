using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER TOUCHES GROUND");
        player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
