using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float impulse;
    [SerializeField] private float gravity;

    private float ground;
    private bool jumping;
    private float yVel;

    private float xMin, xMax;

    // Start is called before the first frame update
    void Start()
    {
        ground = transform.position.y;
        jumping = false;
        yVel = 0;

        xMax = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        xMin = xMax - 2*(xMax - Camera.main.transform.position.x);
        Debug.Log(xMin + ", " + xMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(jumping)
        {
            if(transform.position.y <= ground)
            {
                transform.position.Set(transform.position.x, ground, 0);
                jumping = false;
                yVel = 0;
            }
            else
                yVel -= gravity * Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            yVel = impulse;
        }

        float dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // if(transform.position.x + dx <= xMin || transform.position.x + dx >= xMax)
        //     dx = 0;
        float dy = yVel * Time.deltaTime;
        transform.Translate(dx, dy, 0);
    }
}
