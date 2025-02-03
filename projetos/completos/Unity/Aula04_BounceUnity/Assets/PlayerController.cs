using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 3.5f;
    public float impulse = 75;
    public float gravity = 50;

    private float vy;

    // Start is called before the first frame update
    void Start()
    {
        vy = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float vx = Input.GetAxis("Horizontal") * xSpeed;
        
        vy -= gravity * Time.fixedDeltaTime;
    
        Vector2 v = new Vector2(vx, vy);
        pos += v * Time.fixedDeltaTime;

        if(pos.y < -4)
        {
            pos.y = -4;
            vy = impulse;
        }

        transform.position = pos;
    }
}
