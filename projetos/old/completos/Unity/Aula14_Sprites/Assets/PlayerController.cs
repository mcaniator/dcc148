using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator controller;
    private AnimatorController myController;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
        myController = GetComponent<AnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool running = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5;
        // controller.SetBool("Running", running);

        if(running)
            myController.Play();
        else
            myController.Stop();
    }
}
