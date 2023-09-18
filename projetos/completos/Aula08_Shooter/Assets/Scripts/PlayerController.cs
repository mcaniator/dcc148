using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;

    private ObjectPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new ObjectPool(bulletPrefab, 10);   
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if(Input.GetKey(KeyCode.Q))
            transform.Rotate(0, 0, Time.deltaTime*speed);
        else if(Input.GetKey(KeyCode.E))
            transform.Rotate(0, 0, -Time.deltaTime*speed);

        transform.Translate(dx, dy, 0, Space.World);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetFromPool();
        if(bullet)
        {
            bullet.transform.position = transform.position;
            bullet.transform.localRotation = transform.localRotation;
            bullet.transform.Translate(0.75f, -0.25f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(Mathf.Abs(transform.localScale.z - other.transform.localScale.z) < 0.1 && other.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
}
