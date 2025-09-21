using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    Continuous,
    FollowPlayer,
    CameraJump,
    SmoothJump,
    BoxCentered,
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
    private int cameraFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void MoveContinuously()
    {
        float x = Mathf.Clamp(transform.position.x + speed * Time.deltaTime, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void FollowPlayer()
    {
        float x = Mathf.Clamp(player.position.x, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void Jump()
    {
        int frame = Mathf.FloorToInt((player.position.x - leftEdge.position.x) / (2*halfWidth));
        float x = Mathf.Clamp(leftEdge.position.x + halfWidth + 2*frame*halfWidth, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void SmoothJump()
    {
        int frame = Mathf.FloorToInt((player.position.x - leftEdge.position.x) / (2*halfWidth));

        float smoothX = transform.position.x;
        if(frame != cameraFrame)
        {
            float initialPos = leftEdge.position.x + halfWidth + 2*cameraFrame*halfWidth;
            float finalPos = leftEdge.position.x + halfWidth + 2*frame*halfWidth;
            smoothX = Mathf.Lerp(initialPos, finalPos, t);
            t += 2 * Time.deltaTime;
            if(t >= 1)
            {
                cameraFrame += frame-cameraFrame;
                t = 0;
            }
        }

        float x = Mathf.Clamp(smoothX, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void MoveBox()
    {
        float cameraLeft = transform.position.x - halfWidth;
        float regionLeft = cameraLeft + halfWidth * 0.5f;
        float regionRight = cameraLeft + halfWidth * 1.5f;
        float dx = 0;
        if(player.transform.position.x < regionLeft)
        {
            dx = player.transform.position.x - regionLeft;
        }
        else if(player.transform.position.x > regionRight)
        {
            dx = player.transform.position.x - regionRight;
        }
        float x = Mathf.Clamp(transform.position.x + dx, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void LateUpdate()
    {
        switch(cameraType)
        {
            case CameraType.Continuous: MoveContinuously(); break;
            case CameraType.FollowPlayer: FollowPlayer(); break;
            case CameraType.CameraJump: Jump(); break;
            case CameraType.SmoothJump: SmoothJump(); break;
            case CameraType.BoxCentered: MoveBox(); break;
        }
    }
}
