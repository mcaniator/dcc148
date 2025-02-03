using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Matrix4x4 R = Matrix4x4.Rotate(rb.inertiaTensorRotation);
        Matrix4x4 D = Matrix4x4.Scale(rb.inertiaTensor);

        Matrix4x4 I = R * D * R.transpose;

        Debug.Log(I);

        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Force);
    }
}
