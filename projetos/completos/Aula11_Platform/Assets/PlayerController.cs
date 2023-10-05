using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float impulse;
    [SerializeField] private float gravity;

    private float baseGround;
    private float playerGround;
    private bool jumping;
    private float yVel;

    // Start is called before the first frame update
    void Start()
    {
        baseGround = transform.position.y;
        playerGround = baseGround;
        jumping = false;
        yVel = 0;
    }

    void Jump()
    {
        if(jumping)
        {
            if(transform.position.y <= playerGround)
            {
                if(transform.position.y <= baseGround)
                    playerGround = baseGround;
                
                transform.position = new Vector3(transform.position.x, playerGround, 0);
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
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        float dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        float xMax = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float xMin = xMax - 2*(xMax - Camera.main.transform.position.x);
        if(transform.position.x + dx <= xMin || transform.position.x + dx >= xMax)
            dx = 0;
            
        float dy = yVel * Time.deltaTime;
        transform.Translate(dx, dy, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform" && yVel < 0)
        {
            playerGround = other.transform.position.y;
            playerGround += transform.localScale.y*0.5f + other.transform.localScale.y*0.5f;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        playerGround = baseGround;
        jumping = true;
    }
}
