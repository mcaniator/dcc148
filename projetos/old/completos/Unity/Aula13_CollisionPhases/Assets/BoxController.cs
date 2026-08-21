using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject other;

    private Collider2D playerCollider;
    private Collider2D[] otherColliders;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        otherColliders = other.GetComponentsInChildren<Collider2D>(); // índice 0 é o pai

        Debug.Log(otherColliders.Length);

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

    void BroadPhase()
    {
        if(AABBxCircle(playerCollider.bounds, otherColliders[0].bounds))
        {
            spriteRenderer.color = new Color(255, 255, 0);
            NarrowPhase();
        }
        else
            spriteRenderer.color = new Color(255, 255, 255);
    }

    void NarrowPhase()
    {
        for(int i = 1; i < otherColliders.Length; i++)
        {
            bool collided = false;
            if(otherColliders[i] is BoxCollider2D)
                collided = AABBxAABB(playerCollider.bounds, otherColliders[i].bounds);
            else
                collided = AABBxCircle(playerCollider.bounds, otherColliders[i].bounds);

            if(collided)
            {
                Debug.Log(otherColliders[i].gameObject.name);
                spriteRenderer.color = new Color(255, 0, 0);
                break;
            }
            else
                spriteRenderer.color = new Color(255, 255, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime;

        BroadPhase();

        transform.Translate(dx, dy, 0);
    }
}
