using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float impulse;
    [SerializeField] private float gravity;
    [SerializeField] private bool player2;

    private float ground;
    private bool jumping;
    private float yVel;

    // Start is called before the first frame update
    void Start()
    {
        ground = transform.position.y;
        jumping = false;
        yVel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player2)
        {
            float dx = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            float xMax = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
            float xMin = xMax - 2*(xMax - Camera.main.transform.position.x);
            // if(transform.position.x + dx <= xMin || transform.position.x + dx >= xMax)
            //     dx = 0;
                
            transform.Translate(dx, 0, 0);
        }
        else
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
            
            float xMax = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
            float xMin = xMax - 2*(xMax - Camera.main.transform.position.x);
            // if(transform.position.x + dx <= xMin || transform.position.x + dx >= xMax)
            //     dx = 0;
                
            float dy = yVel * Time.deltaTime;
            transform.Translate(dx, dy, 0);
        }
    }
}
