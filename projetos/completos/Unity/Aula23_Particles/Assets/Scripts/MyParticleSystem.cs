using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleSystem : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private int numberOfParticles;

    private Particle[] particles;
    private int active = 0;

    // Start is called before the first frame update
    void Start()
    {
        particles = new Particle[numberOfParticles];

        for(int i = 0; i < numberOfParticles; i++) 
        {
            particles[i] = new Particle(Object.Instantiate(particlePrefab), 2);
            particles[i].Restart();
        }
        active = numberOfParticles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < numberOfParticles; i++)
        {
            particles[i].Update();
            if(!particles[i].Active)
                particles[i].Restart();
        }
    }
}
