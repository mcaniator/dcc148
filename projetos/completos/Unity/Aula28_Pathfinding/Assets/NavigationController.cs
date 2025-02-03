using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavigationController : MonoBehaviour
{
    private Tilemap map;

    public Vector3Int start;
    public Vector3Int end;

    void AStar()
    {
        Vector3Int pos = start;
        Vector3Int[] neighbors = {new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(0, 1, 0)};
        int it = 0;
        while(pos != end && it < 100)
        {
            float minDist = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
            Vector3Int bestNext = pos;
            foreach(Vector3Int neighbor in neighbors)
            {
                Vector3Int next = pos + neighbor;
                float distFromStart = Mathf.Abs(next.x - start.x) + Mathf.Abs(next.y - start.y);
                float distFromEnd = Mathf.Abs(next.x - end.x) + Mathf.Abs(next.y - end.y);
                float distSum = distFromStart + distFromEnd;
                if(distSum <= minDist && map.GetColor(next) == Color.white)
                {
                    minDist = distSum;
                    bestNext = next;
                }
            }
            pos = bestNext;
            map.SetTileFlags(pos, TileFlags.None);
            map.SetColor(pos, Color.yellow);
            Debug.Log(minDist + ", " + pos);
            it++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();

        map.SetTileFlags(start, TileFlags.None);
        map.SetColor(start, Color.green);
        map.SetTileFlags(end, TileFlags.None);
        map.SetColor(end, Color.red);
        AStar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
