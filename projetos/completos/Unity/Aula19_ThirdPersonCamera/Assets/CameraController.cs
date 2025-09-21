using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private enum CameraMode
    {
        Slave,
        Mouse
    }

    [SerializeField] private Transform player;

    [SerializeField] private float camDistance;
    [SerializeField] private float camHeight;

    [SerializeField] private CameraMode cameraMode;

    void SlaveMode()
    {
        Vector3 camPos = player.transform.position - player.transform.forward * camDistance;
        
        camPos += Vector3.up * camHeight;
        
        transform.position = camPos;
        transform.localRotation = player.transform.localRotation;
    }

    void MouseMode()
    {
        if(Input.GetButton("Fire1"))
        {
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X"));
        }
        
        float dz = Input.GetAxis("Mouse ScrollWheel");
        camDistance += dz;

        Vector3 camPos = player.transform.position - transform.forward * camDistance;
        
        camPos += Vector3.up * camHeight;
        
        transform.position = camPos;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch(cameraMode)
        {
            case CameraMode.Slave: SlaveMode(); break;
            case CameraMode.Mouse: MouseMode(); break;
        }
    }
}
