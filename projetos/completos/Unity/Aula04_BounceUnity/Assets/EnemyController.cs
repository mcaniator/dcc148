using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;

    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        pos.x += speed * Time.deltaTime * direction;
        
        if(pos.x > 8)
        {
            speed *= 1.05f;
            direction = -1;
        }
        else if(pos.x < -8)
        {
            speed *= 1.05f;
            direction = 1;
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
