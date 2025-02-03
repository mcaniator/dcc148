using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int motionRadius;
    
    private Grid grid;
    private Tilemap map;
    private bool selected = false;
    private bool moving = false;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        map = GameObject.Find("PlayerSelection").GetComponent<Tilemap>();

        Vector3Int currentCell = grid.WorldToCell(transform.position);
        transform.position = grid.CellToWorld(currentCell);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if(hit.transform)
            {
                if(hit.transform.gameObject.tag == "Player")
                {
                    selected = true;
                    moving = false;

                    Vector3Int currentCell = grid.WorldToCell(transform.position);
                    Debug.Log(currentCell);
                    // for(int i = -motionRadius; i <= motionRadius; i++)
                    // {
                    //     for(int j = -motionRadius; j <= motionRadius; j++)
                    //     {
                    //         int x = currentCell.x + i;
                    //         int y = currentCell.y + j;
                    //         map.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                    //         map.SetColor(new Vector3Int(x, y, 0), new Color(1, 0, 0, 0.9f));
                    //     }
                    // }
                    for(int i = -motionRadius; i <= motionRadius; i++)
                    {
                        for(int j = -motionRadius; j <= motionRadius; j++)
                        {
                            Vector3Int tilePos = new Vector3Int(currentCell.x + i, currentCell.y + j, 0);
                            if(Vector3Int.Distance(currentCell, tilePos) <= motionRadius)
                            {
                                map.SetTileFlags(new Vector3Int(tilePos.x, tilePos.y, 0), TileFlags.None);
                                map.SetColor(new Vector3Int(tilePos.x, tilePos.y, 0), new Color(1, 0, 0, 0.9f));
                            }
                        }
                    }
                }
            }
            else if(selected)
            {
                destination = worldPos;
                destination.z = 0;
                moving = true;
            }
        }
        if(moving)
        {
            float dist = Vector3.Distance(transform.position, destination);
            if(dist > 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime);
            else
            {
                selected = false;
                moving = false;

                Tilemap water = GameObject.Find("Water").GetComponent<Tilemap>();
                water.SetTile(new Vector3Int(-9, 0), null);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other);
    }
}
