using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    Continuous,
    FollowPlayer,
    // INCLUA AQUI OUTROS TIPOS DE CÂMERA QUE VOCÊ IMPLEMENTAR
}

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CameraType cameraType;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private Transform player;
    private float halfWidth;
    private float halfHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void MoveCamera()
    {
        Debug.Log("Continuous");
        // INCLUA AQUI SEU CÓDIGO
    }

    void FollowPlayer()
    {
        Debug.Log("Follow");
        // INCLUA AQUI SEU CÓDIGO
    }

    void LateUpdate()
    {
        switch(cameraType)
        {
            case CameraType.Continuous: MoveCamera(); break;
            case CameraType.FollowPlayer: FollowPlayer(); break;
        }
    }
}
