using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    private GameObject obj;
    private Rigidbody rb;
    private float lifetime;

    private float timeLeft;
    private bool active;

    public bool Active
    {
        get { return active; }
    }

    public Particle(GameObject obj, float lifetime)
    {
        this.obj = obj;
        this.rb = obj.GetComponent<Rigidbody>();
        this.lifetime = lifetime;

        timeLeft = lifetime;
        active = false;
    }

    public void Restart()
    {
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        obj.transform.Rotate(0, 0, Random.Range(-45, 45));
        rb.velocity = Vector3.zero;
        rb.AddForce(obj.transform.up * Random.Range(1, 10), ForceMode.Impulse);
        timeLeft = lifetime;
        active = true;
    }

    public void Update()
    {
        if(active)
        {
            ReorientBillboard();
            timeLeft -= Time.fixedDeltaTime;
            if(timeLeft <= 0)
                active = false;
        }
    }

    void ReorientBillboard()
    {
        obj.transform.LookAt(Camera.main.transform);
        obj.transform.Rotate(0, 180, 0);
    }
}
