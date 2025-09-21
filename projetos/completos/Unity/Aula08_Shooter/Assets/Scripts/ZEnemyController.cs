using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZEnemyController : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float zSpeed;

    private SpriteRenderer objRenderer;
    private bool lastFrameActive;

    void Reset()
    {
        if(Random.Range(0.0f, 1.0f) < 0.5)
            transform.localScale = new Vector3(0, 0, 0);
        else
            transform.localScale = new Vector3(10, 10, 10);
        
        lastFrameActive = true;
    }

    void Kill()
    {
        gameObject.SetActive(false);
        lastFrameActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastFrameActive == false && gameObject.activeSelf)
            Reset();

        float ds = 0; 
        if(transform.localScale.x < 1)
            ds = Time.deltaTime * zSpeed;
        else if(transform.localScale.x > 1)
            ds = -Time.deltaTime * zSpeed * 10f;
        
        Vector3 vs = new Vector3(ds, ds, ds);
        transform.localScale += vs;

        transform.Translate(-Time.deltaTime*xSpeed, 0, 0);

        if(transform.position.x < -9)
            Kill();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(Mathf.Abs(transform.localScale.z - other.transform.localScale.z) < 0.1)
            Kill();
    }
}
