using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float eyeSpeed;
    [SerializeField] private GameObject bulletPrefab;

    private Quaternion baseOrientation;
    private float mouseH = 0;
    private float mouseV = 0;
    private GameObject bullet;
    private Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        baseOrientation = transform.localRotation;  
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bullet = Object.Instantiate(bulletPrefab);
        bullet.SetActive(false);
    }

    void Shoot()
    {
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        Vector3 targetScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 targetWorldPos = Camera.main.ScreenToWorldPoint(targetScreenPos);
        targetVector = (targetWorldPos - bullet.transform.position).normalized;
        bullet.transform.localRotation = Quaternion.FromToRotation(Vector3.up, targetVector);
    }

    // Update is called once per frame
    void Update()
    {
        mouseH += Input.GetAxis("Mouse X");
        mouseV += Input.GetAxis("Mouse Y");

        Quaternion rotY = Quaternion.AngleAxis(mouseH * eyeSpeed, Vector3.up);
        Quaternion rotX = Quaternion.AngleAxis(mouseV * eyeSpeed, Vector3.left);
        transform.localRotation = baseOrientation * rotY * rotX;

        // OUTRAS POSSIBILIDADES
        
        // transform.localRotation = baseOrientation;
        // transform.Rotate(Vector3.right, mouseV);
        // transform.Rotate(Vector3.up, mouseH, Space.World);

        // Quaternion rot = Quaternion.Euler(mouseV, mouseH, 0);
        // transform.localRotation = rot;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
