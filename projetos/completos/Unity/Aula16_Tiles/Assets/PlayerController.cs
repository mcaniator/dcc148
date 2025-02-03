using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Grid grid;
    private Vector3Int currentCell;
    private Vector3Int destination;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();

        currentCell = grid.WorldToCell(transform.position);
        transform.position = grid.CellToWorld(currentCell);

        Debug.Log(grid.cellSize);

        Tilemap map = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        map.SetTileFlags(new Vector3Int(1, 0, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(2, 0, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(1, 1, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(0, 1, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(0, 2, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(-1, 1, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(-1, 0, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(-2, 0, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(-1, -1, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(0, -1, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(0, -2, 0), TileFlags.None);
        map.SetTileFlags(new Vector3Int(1, -1, 0), TileFlags.None);
        
        map.SetColor(new Vector3Int(1, 0, 0), Color.red);
        map.SetColor(new Vector3Int(2, 0, 0), Color.red);
        map.SetColor(new Vector3Int(1, 1, 0), Color.red);
        map.SetColor(new Vector3Int(0, 1, 0), Color.red);
        map.SetColor(new Vector3Int(0, 2, 0), Color.red);
        map.SetColor(new Vector3Int(-1, 1, 0), Color.red);
        map.SetColor(new Vector3Int(-1, 0, 0), Color.red);
        map.SetColor(new Vector3Int(-2, 0, 0), Color.red);
        map.SetColor(new Vector3Int(-1, -1, 0), Color.red);
        map.SetColor(new Vector3Int(0, -1, 0), Color.red);
        map.SetColor(new Vector3Int(0, -2, 0), Color.red);
        map.SetColor(new Vector3Int(1, -1, 0), Color.red);
    }

    void SnapToCellCenter()
    {
        transform.position = new Vector3(currentCell.x + 0.5f, currentCell.y + 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        Debug.Log(Mathf.Floor(-2.3f));

        if(Mathf.Abs(dx) > 0.1f || Mathf.Abs(dy) > 0.1f)
        {
            direction = new Vector3(dx, dy, 0);
            transform.position += direction * speed * Time.deltaTime;
            currentCell = grid.WorldToCell(transform.position);
            destination = currentCell + Vector3Int.FloorToInt(direction);
        }
        else
        {   
            Debug.Log(currentCell + " + " + direction + " = " + (currentCell + Vector3Int.FloorToInt(direction)));
            Debug.Log(currentCell + " --> " + destination + " | " + transform.position + " --> " + grid.CellToWorld(destination));
            transform.position = Vector3.Lerp(transform.position, grid.CellToWorld(destination) + direction, Time.deltaTime * speed);
            currentCell = grid.WorldToCell(transform.position);
        }

        // float x = transform.position.x;
        // float y = transform.position.y;
        // if(Mathf.Abs(dx) > 0.1f)
        // {
        //     x += dx * speed * Time.deltaTime;
        //     direction.x = dx;

        //     if(Mathf.Abs(x-(int)x)*dx > 0.5f*dx)
        //         destination.x = currentCell.x + (int)dx;
        // }
        
        // if(Mathf.Abs(dy) > 0.1f)
        // {
        //     y += dy * speed * Time.deltaTime;
        //     direction.y = dy;

        //     if(Mathf.Abs(y-(int)y)*dy > 0.5f*dy)
        //         destination.y = currentCell.y + (int)dy;
        // }
        
        // if(Mathf.Abs(dx) > 0.1f || Mathf.Abs(dy) > 0.1f)
        //     transform.position = new Vector3(x, y, 0);
        // else
        // {
        //     if(currentCell != destination)
        //         transform.position += direction * speed * Time.deltaTime;
        //     // else
        //     //     SnapToCellCenter();
        // }
        // currentCell = grid.WorldToCell(transform.position);
        // Debug.Log(currentCell + " --> " + destination);
    }
}
