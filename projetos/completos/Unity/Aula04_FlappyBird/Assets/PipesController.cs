using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesController : MonoBehaviour
{
    public float speed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(8, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -8)
        {
            float y = Random.Range(-4.0f, 4.0f);
            transform.position = new Vector3(8, y, 0);
        }
        
        Vector3 dx = new Vector3(speed*Time.deltaTime, 0);
        transform.position -= dx;
    }
}
