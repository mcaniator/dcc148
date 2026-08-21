using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime * 50, 0, Space.Self);
    }
}
