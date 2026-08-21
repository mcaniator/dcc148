using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject other;

    private Collider2D col1;
    private Collider2D col2;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        col1 = GetComponent<Collider2D>();
        col2 = other.GetComponent<Collider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool AABBxAABB(Bounds b1, Bounds b2)
    {
        if( b1.max.x >= b2.min.x && 
            b1.min.x <= b2.max.x && 
            b1.max.y >= b2.min.y && 
            b1.min.y <= b2.max.y )
            return true;
        else
            return false;
    }

    bool CirclexCircle(Bounds b1, Bounds b2)
    {
        float r1 = b1.extents.x;
        float r2 = b2.extents.x;

        float dx = b2.center.x - b1.center.x;
        float dy = b2.center.y - b1.center.y;

        if( dx*dx + dy*dy <= (r1 + r2)*(r1 + r2) )
            return true;
        else
            return false;
    }

    bool AABBxCircle(Bounds bb, Bounds bc)
    {
        float r = bc.extents.x;

        float nearestBoxX = Mathf.Min( Mathf.Max( bb.min.x, bc.center.x ), bb.max.x);
        float nearestBoxY = Mathf.Min( Mathf.Max( bb.min.y, bc.center.y ), bb.max.y);

        float dx = nearestBoxX - bc.center.x;
        float dy = nearestBoxY - bc.center.y;

        if( dx*dx + dy*dy <= r*r )
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime;

        bool collided = false;
        if(col2 is BoxCollider2D)
            collided = AABBxAABB(col1.bounds, col2.bounds);
        else
            collided = AABBxCircle(col1.bounds, col2.bounds);
        
        if(collided)
            spriteRenderer.color = new Color(255, 0, 0);
        else
            spriteRenderer.color = new Color(255, 255, 255);

        transform.Translate(dx, dy, 0);
    }
}
