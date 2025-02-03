using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Quaternion q;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle = Mathf.PI/4; // 45
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = Quaternion.identity;
        float qangle = angle * 0.5f;
        q.w = Mathf.Cos(qangle);
        q.x = 0;
        q.y = 0;
        q.z = Mathf.Sin(qangle);
        // transform.localRotation = q;
        
        // q.x = Mathf.Sin(qangle);
        // q.y = 0;
        // q.z = 0;
        // transform.localRotation *= q;

        Vector3 toPivot = transform.position - Vector3.zero;
        // transform.Translate(-2, 0, 0);
        // Vector3 rotatedPivot = q * toPivot;
        // q = Quaternion.FromToRotation(toPivot, rotatedPivot);
        // transform.localRotation *= q;
        // toPivot = q * toPivot;
        // Debug.Log(toPivot);
        // transform.position = toPivot;
        // transform.Translate(2, 0, 0);

        // transform.RotateAround(Vector3.zero, Vector3.forward, 45);

        transform.Translate(-2, 0, 0);
        transform.Rotate(0, 0, angle);
        transform.Translate(2, 0, 0);
        Debug.Log(transform.localRotation);
    }
}
