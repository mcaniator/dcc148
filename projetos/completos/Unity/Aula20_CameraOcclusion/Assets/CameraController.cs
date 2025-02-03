using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum OcclusionCheck
    {
        OnePoint,
        ThreePoints,
        FivePoints
    }

    [SerializeField] private Transform target;

    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private OcclusionCheck occlusionTest;

    private float adjustedDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        adjustedDistance = distance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOcclusion();

        PositionAndRotate();
    }

    void CheckOcclusion()
    {
        switch(occlusionTest)
        {
            case OcclusionCheck.OnePoint: OnePointOcclusion(); break;
            case OcclusionCheck.ThreePoints: ThreePointOcclusion(); break;
            case OcclusionCheck.FivePoints: FivePointOcclusion(); break;
        }
    }

    void OnePointOcclusion()
    {
        Vector3 expectedPosition = target.position - target.forward * distance + Vector3.up * height;
        Ray ray = new Ray(target.position, (expectedPosition - target.position).normalized);
        RaycastHit hit;
        Debug.DrawLine(target.position, expectedPosition, Color.green);
        if(Physics.Raycast(ray, out hit, distance, collisionLayer))
            adjustedDistance = hit.distance;
        else
            adjustedDistance = distance;
    }

    void ThreePointOcclusion()
    {
        Vector3 expectedPosition = target.position - target.forward * distance + Vector3.up * height;

        Vector3[] targetParts = new Vector3[3];
        targetParts[0] = target.position;                     // pés (o objeto usado está com a posição alinhada com os pés)
        targetParts[1] = target.position + Vector3.up * 0.5f; // tronco (centro)
        targetParts[2] = target.position + Vector3.up;        // cabeça

        int collisionCount = 0;
        float maxDist = 0;
        for(int i = 0; i < targetParts.Length; i++)
        {
            Ray ray = new Ray(targetParts[i], (expectedPosition - targetParts[i]).normalized);
            RaycastHit hit;
            Debug.DrawLine(targetParts[i], expectedPosition, Color.green);
            if(Physics.Raycast(ray, out hit, distance, collisionLayer))
            {
                collisionCount++;
            
                if(hit.distance > maxDist)
                    maxDist = hit.distance;
            }
        }
        if(collisionCount == targetParts.Length)
            adjustedDistance = maxDist;
        else
            adjustedDistance = distance;
    }

    void FivePointOcclusion()
    {
        Vector3 expectedPosition = target.position - target.forward * distance + Vector3.up * height;

        Vector3[] camPoints = new Vector3[5];
        camPoints[0] = expectedPosition + Vector3.forward * Camera.main.nearClipPlane;

        float halfHeight = Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad) * Camera.main.nearClipPlane;
        float halfWidth = halfHeight * Camera.main.aspect;

        Debug.Log(halfHeight);
        Debug.Log(halfWidth);
        camPoints[1] = camPoints[0] - transform.right * halfWidth;
        camPoints[2] = camPoints[0] + transform.right * halfWidth;
        camPoints[3] = camPoints[0] - transform.up * halfWidth;
        camPoints[4] = camPoints[0] + transform.up * halfWidth;

        adjustedDistance = distance;
        for(int i = 0; i < camPoints.Length; i++)
        {
            Ray ray = new Ray(target.position, (camPoints[i] - target.position).normalized);
            RaycastHit hit;
            Debug.DrawLine(target.position, camPoints[i], Color.green);
            if(Physics.Raycast(ray, out hit, distance, collisionLayer))
            {
                adjustedDistance = hit.distance;
                break;
            }
        }
    }

    void PositionAndRotate()
    {
        transform.position = target.position - target.forward * adjustedDistance;
        transform.position += Vector3.up * height;

        transform.LookAt(target);
    }
}
