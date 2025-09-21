using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 3.5f;
    public float impulse = 75;
    public float gravity = 50;

    public TMP_Text pointsTmp;

    private float vy;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        vy = 0;
        points = 0;
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
            points++;
            pointsTmp.text = points.ToString();
        }

        transform.position = pos;
    }
}
