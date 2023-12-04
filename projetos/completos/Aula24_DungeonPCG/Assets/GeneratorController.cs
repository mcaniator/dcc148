using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public int subdivisionMaxLevel;

    private MeshRenderer floorRenderer;
    private MeshRenderer wallRenderer;

    public void CreateFixedSizeRoom(int i, int j)
    {
        GameObject room = Object.Instantiate(floorPrefab);
        Vector3 pos = new Vector3(i, 0, j);
        Vector3 roomSize = floorRenderer.bounds.extents*2;
        room.transform.position = Vector3.Scale(pos, roomSize);
        room.transform.parent = transform;
    }

    public void CreateAnySizeRoom(int i, int j, Vector3 roomScale)
    {
        GameObject room = Object.Instantiate(floorPrefab);
        Vector3 pos = new Vector3(i, 0, j);
        Vector3 roomSize = floorRenderer.bounds.extents*2;
        room.transform.position = Vector3.Scale(pos, roomSize);
        room.transform.localScale = roomScale * 0.1f;
        room.transform.parent = transform;
    }

    public void CreateWalls()
    {
        Object.Instantiate(wallPrefab);
    }

    public void Generate()
    {
        BinarySubdivision(new Vector2Int(0, 0), new Vector2Int(100, 100), subdivisionMaxLevel);
    }

    public void BinarySubdivision(Vector2Int min, Vector2Int max, int level)
    {
        if(level > 0)
        {
            Debug.Log(min + ", " + max);

            int x = Random.Range(min.x, max.x);
            int y = Random.Range(min.y, max.y);

            bool divideY = (Random.value <= 0.5f) ? true : false;

            if(divideY)
            {
                BinarySubdivision(new Vector2Int(min.x, min.y), new Vector2Int(max.x, y-1), level-1);
                BinarySubdivision(new Vector2Int(min.x, y), new Vector2Int(max.x, max.y), level-1);
            }
            else
            {
                BinarySubdivision(new Vector2Int(min.x, min.y), new Vector2Int(x-1, max.x), level-1);
                BinarySubdivision(new Vector2Int(x, min.y), new Vector2Int(max.x, max.y), level-1);
            }
        }

        if(level == 1)
        {
            int xMin = Random.Range(min.x, max.x);
            int xMax = Random.Range(min.x, max.x);
            int yMin = Random.Range(min.y, max.y);
            int yMax = Random.Range(min.y, max.y);

            int w = Mathf.Abs(xMax-xMin);
            int h = Mathf.Abs(yMax-yMin);
            Debug.Log(w + ", " + h);
            Vector3 roomSize = new Vector3(Mathf.Clamp(w, 1, w), 1, Mathf.Clamp(h, 1, h));
            int xc = (xMin + xMax) / 2;
            int yc = (yMin + yMax) / 2;
            CreateAnySizeRoom(xc, yc, roomSize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        floorRenderer = floorPrefab.GetComponent<MeshRenderer>();
        wallRenderer = wallPrefab.GetComponent<MeshRenderer>();
        Debug.Log(floorRenderer.bounds.extents);
        Generate();
    }
}
