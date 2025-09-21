using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector2 direction;

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    void Start()
    {
        direction = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        if(dx < 0)
            direction.x = -1;
        else if(dx > 0)
            direction.x = 1;
        
        transform.Translate(dx, 0, 0);
    }
}
