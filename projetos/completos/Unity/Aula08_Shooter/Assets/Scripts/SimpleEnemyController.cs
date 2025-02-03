using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Kill()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed*Time.deltaTime, 0, 0);
        if(transform.position.x < -9)
            Kill();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Kill();
    }
}
