using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * velocidade;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * velocidade;

        transform.position = pos;
    }
}
