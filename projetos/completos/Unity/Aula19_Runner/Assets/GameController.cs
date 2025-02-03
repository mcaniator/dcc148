using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    private GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        cube = Object.Instantiate(cubePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.position = new Vector3(0, 1, 5);
    }
}
