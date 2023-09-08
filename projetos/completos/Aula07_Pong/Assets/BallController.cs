using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-0.5f, 0.5f);
        direction.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
        if(transform.position.y >= 5)
            direction = Vector2.Reflect(direction, Vector2.down);
        else if(transform.position.y <= -5)
            direction = Vector2.Reflect(direction, Vector2.up);            

    }

    void OnCollisionEnter2D(Collision2D ball) 
    {
        // Por simplicidade, estamos apenas refletindo a bola em torno da normal. 
        // No Pong original, a mecânica não é exatamente esta.
        ContactPoint2D contact = ball.GetContact(0);
        direction = Vector2.Reflect(direction, contact.normal);
    }
}
