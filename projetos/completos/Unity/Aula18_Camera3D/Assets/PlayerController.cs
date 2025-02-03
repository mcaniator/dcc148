using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        float dz = Input.GetAxis("Depth");

        Vector3 playerPos = transform.position;
        Vector3 camPos = Camera.main.transform.position;

        float fovDiv2 = Camera.main.fieldOfView * Mathf.Deg2Rad * 0.5f;
        float meiaAltura = Mathf.Abs(playerPos.z - camPos.z) * Mathf.Tan(fovDiv2);
        float meiaLargura = Camera.main.aspect * meiaAltura;

        Debug.Log(meiaLargura + " - " + meiaAltura);

        if(playerPos.x < camPos.x-meiaLargura && dx < 0 ||
           playerPos.x > camPos.x+meiaLargura && dx > 0)
           dx = 0;

        if(playerPos.y < camPos.y-meiaAltura && dy < 0 ||
           playerPos.y > camPos.y+meiaAltura && dy > 0)
           dy = 0;

        transform.Translate(dx, dy, dz);
    }
}
