using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    public float Speed
    {
        get { return speed; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dy = Input.GetAxis("Vertical") * Time.fixedDeltaTime * speed;
        transform.Translate(0, dy, 0);
    }
}
