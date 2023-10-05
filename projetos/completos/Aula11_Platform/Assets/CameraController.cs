using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    FollowPlayer,
    FollowPlatform
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

    private float t = 0;
    private float baseHeight;
    private float previousHeight;
    private float playerBase;
    private bool transitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        baseHeight = transform.position.y;
        playerBase = player.position.y;
    }

    void FollowPlayer()
    {
        float x = Mathf.Clamp(player.position.x, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = Mathf.Clamp(player.position.y, baseHeight, baseHeight + halfWidth);
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);

        previousHeight = player.position.y;
    }

    void FollowPlatform()
    {
        float x = Mathf.Clamp(player.position.x, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
                
        float y = transform.position.y;
        if(!transitioning)
        {
            if(Mathf.Approximately(previousHeight, player.position.y) && !Mathf.Approximately(playerBase, player.position.y))
                transitioning = true;
        }
        else
        {
            t += speed * Time.deltaTime;
            y = Mathf.Lerp(playerBase, player.position.y, t);
            y = baseHeight + y;
            if(t >= 1)
            {
                playerBase = player.position.y;
                transitioning = false;
                t = 0;
            }
        }
        y = Mathf.Clamp(y, baseHeight, baseHeight + halfWidth);

        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);

        previousHeight = player.position.y;
    }

    void LateUpdate()
    {
        switch(cameraType)
        {
            case CameraType.FollowPlayer: FollowPlayer(); break;
            case CameraType.FollowPlatform: FollowPlatform(); break;
        }
    }
}
