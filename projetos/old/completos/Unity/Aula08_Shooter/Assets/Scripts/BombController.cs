using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private float speed;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        gravity = 9.8f;
    }

    void Reset()
    {
        speed = 1;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dy = -Time.deltaTime * speed;
        transform.Translate(0, dy, 0);
        speed += gravity * Time.deltaTime;

        if(transform.position.y < -5)
            Reset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag != other.gameObject.tag)
            Reset();
    }
}
