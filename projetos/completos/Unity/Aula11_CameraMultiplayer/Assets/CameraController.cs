using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CameraType
{
    Average,
    Split
}

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CameraType cameraType;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Camera player2Camera;

    private GameObject[] players;
    private float halfWidth;
    private float halfHeight;

    private float t = 0;
    private int cameraFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void KeepAllInFrame()
    {
        float x = 0; 
        for(int i = 0; i < players.Length; i++)
        {
            x += players[i].transform.position.x;
        }
        x /= players.Length;
        x = Mathf.Clamp(x, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = transform.position.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    void SplitScreen()
    {
        if(Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x) > 2*halfWidth)
        {
            float y = transform.position.y;
            float z = transform.position.z;
            
            Camera.main.rect = new Rect(0, 0, 0.5f, 1);

            float x = Mathf.Clamp(players[1].transform.position.x, leftEdge.position.x + halfWidth*0.5f, rightEdge.position.x - halfWidth*0.5f);
            Debug.Log(x);
            transform.position = new Vector3(x, y, z);
            
            x = Mathf.Clamp(players[0].transform.position.x, leftEdge.position.x + halfWidth*0.5f, rightEdge.position.x - halfWidth*0.5f);
            Debug.Log("2: " + x);
            player2Camera.gameObject.SetActive(true);
            player2Camera.gameObject.transform.position = new Vector3(x, y, z);
        }
        else
        {
            player2Camera.gameObject.SetActive(false);
            Camera.main.rect = new Rect(0, 0, 1, 1);
            KeepAllInFrame();
        }
    }

    void LateUpdate()
    {
        switch(cameraType)
        {
            case CameraType.Average: KeepAllInFrame(); break;
            case CameraType.Split: SplitScreen(); break;
        }
    }
}
