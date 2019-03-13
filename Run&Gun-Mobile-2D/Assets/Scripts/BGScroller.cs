using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
    public float position;
    public float speed;
    Vector3 startPos;
    public bool startMoving = false;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);

            if (transform.position.x < position)
                transform.position = startPos;

        }
    }
}


    /*
    public float scrollSpeed;
    public float tileSizeZ;

    public bool startMoving = false;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (startMoving)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + Vector3.forward * newPosition;
        }
    }
}*/

    /*
    public float speed = 0.5f;
    public bool startMoving = false;

    private Renderer rend;

    private void Start()
    {
        //  renderer = GetComponent<MeshRenderer>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if(startMoving)
        {
            Vector2 offset = new Vector2(Time.time * speed, 0);

            rend.material.mainTextureOffset = offset;
        }
      
    }
}*/