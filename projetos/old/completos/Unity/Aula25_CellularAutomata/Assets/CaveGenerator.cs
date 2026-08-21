using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveGenerator : MonoBehaviour
{
    public TileBase tile;
    public int width;
    public int height;
    public int maxIterations;

    private Tilemap map;
    private bool[,] grid;


    // Start is called before the first frame update
    void Start()
    {
        map = GetComponentInChildren<Tilemap>();
        BuildMap();
        FillTiles();
    }

    void BuildMap()
    {
        grid = new bool[width, height];
        bool[,] oldGrid = new bool[width, height];

        // inicializa a grade aleatoriamente
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(Random.value <= 0.5f)
                    grid[i, j] = true;
                else
                    grid[i, j] = false;
            }
        }

        for(int n = 0; n < maxIterations; n++)
        {
            (grid, oldGrid) = (oldGrid, grid);

            for(int i = 1; i < width-1; i++)
            {
                for(int j = 1; j < height-1; j++)
                {
                    int count = 0;
                    for(int di = -1; di <= 1; di++)
                    {
                        for(int dj = -1; dj <= 1; dj++)
                        {
                            if(oldGrid[i+di, j+dj])
                                count++;
                        }
                    }
                    if(count >= 5)
                        grid[i, j] = true;
                    else
                        grid[i, j] = false;
                }
            }
        }
    }

    void FillTiles()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(grid[i, j])
                {
                    map.SetTileFlags(new Vector3Int(i, j, 0), TileFlags.None);
                    map.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
