using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinEnemyController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Kill()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed);

        float x = transform.position.x - speed * Time.deltaTime;
        float y = 4*Mathf.Sin(transform.position.x);

        transform.position = new Vector2(x, y);

        if(transform.position.x < -9)
            Kill();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Kill();
    }
}
