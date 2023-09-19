using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*Time.deltaTime, 0, 0);
        if(transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 5)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag != other.gameObject.tag)
            gameObject.SetActive(false);
    }
}
