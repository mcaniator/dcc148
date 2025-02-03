using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //     Debug.Log(target.transform.position);
    //     Debug.Log(target.anchoredPosition);
    //     Debug.Log(Input.mousePosition);
        target.anchoredPosition = Input.mousePosition;

        if(Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if(hit.transform)
            {
                Debug.Log(hit.transform.gameObject);
            }
        }
    }
}
