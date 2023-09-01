using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float impulse = 15;
    public float gravity = 40;

    private float vy;

    // Start is called before the first frame update
    void Start()
    {
        vy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        
        vy -= gravity * Time.deltaTime;
        pos.y += vy * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            vy = impulse;
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
