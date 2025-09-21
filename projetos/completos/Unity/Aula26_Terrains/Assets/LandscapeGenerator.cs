using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandscapeGenerationMethod
{
    ValueNoiseRaw,
    ValueNoiseBilinear,
    ValueNoiseBicubic,
    PerlinNoise,
    FaultFormation,
    DiamondSquare
}

public class LandscapeGenerator : MonoBehaviour
{
    public LandscapeGenerationMethod method;
    public int largeScale;
    public float roughness;
    public float initialHeight;
    public int faultIterations;
    public float amplitude;
    public float frequency;

    private float[,] heights;
    private int width, height;
    private Terrain terrain;

    void BilinearInterpolation()
    {
        float[,] aux = heights;
        heights = new float[width, height];

        int largeScaleWidth = width / largeScale;
        int largeScaleHeight = height / largeScale;
        for(int i = 0; i < largeScaleWidth * largeScale; i++)
        {
            for(int j = 0; j < largeScaleHeight * largeScale; j++)
            {
                int iLeft = (i / largeScale)*largeScale;
                int iRight = iLeft + largeScale;
                int jBottom = (j / largeScale)*largeScale;
                int jTop = jBottom + largeScale;

                float s0 = (iRight - i) * 1.0f/largeScale;
                float s1 = 1.0f - s0;
                float t0 = (jTop - j) * 1.0f/largeScale;
                float t1 = 1.0f -t0;

                float hBottom = aux[iLeft, jBottom]*s0 + aux[iRight, jBottom]*s1;
                float hTop = aux[iLeft, jTop]*s0 + aux[iRight, jTop]*s1;
                float h = hBottom * t0 + hTop * t1;
                heights[i, j] = h;
            }
        }
    }

    float cubic(float x)
    {
        return -2*x*x*x + 3*x*x;
    }
    
    void BicubicInterpolation()
    {
        float[,] aux = heights;
        heights = new float[width, height];

        int largeScaleWidth = width / largeScale;
        int largeScaleHeight = height / largeScale;
        for(int i = 0; i < largeScaleWidth * largeScale; i++)
        {
            for(int j = 0; j < largeScaleHeight * largeScale; j++)
            {
                int iLeft = (i / largeScale)*largeScale;
                int iRight = iLeft + largeScale;
                int jBottom = (j / largeScale)*largeScale;
                int jTop = jBottom + largeScale;

                float s0 = cubic((iRight - i) * 1.0f/largeScale);
                float s1 = cubic((i - iLeft) * 1.0f/largeScale);
                float t0 = cubic((jTop - j) * 1.0f/largeScale);
                float t1 = cubic((j - jBottom) * 1.0f/largeScale);

                float hBottom = aux[iLeft, jBottom]*s0 + aux[iRight, jBottom]*s1;
                float hTop = aux[iLeft, jTop]*s0 + aux[iRight, jTop]*s1;
                float h = hBottom * t0 + hTop * t1;
                heights[i, j] = h;
            }
        }
    }

    void ValueNoise(int interpolationMethod = 0)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(interpolationMethod == 0)
                    heights[i, j] = Random.value;
                else if(i % largeScale == 0 && j % largeScale == 0)
                    heights[i, j] = Random.value;
                else
                    heights[i, j] = 0;
            }
        }

        if(interpolationMethod == 1)
            BilinearInterpolation();
        else if(interpolationMethod == 2)
            BicubicInterpolation();
    }

    void PerlinNoise()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                float x = (float)i / width * frequency;
                float y = (float)j / height * frequency;
                heights[i, j] = amplitude * Mathf.PerlinNoise(x, y);
            }
        }
    }

    void FaultFormation()
    {
        int maxIterations = faultIterations;
        float h = initialHeight;
        float heightDamp = 0.1f;
        for(int n = 0; n < maxIterations; n++)
        {
            Vector3 p0 = new Vector3(Random.Range(0, width-1), Random.Range(0, height-1), 0);
            Vector3 p1 = new Vector3(Random.Range(0, width-1), Random.Range(0, height-1), 0);
            Vector3 lineVector = (p1 - p0);
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    Vector3 pos = new Vector3(i, j, 0);
                    Vector3 pointToLine = (pos - p0);
                    Vector3 cross = Vector3.Cross(lineVector, pointToLine);

                    if(cross.z < 0)
                        heights[i, j] += h;
                }
            }
            h -= heightDamp;
        }
    }

    void DiamondSquare()
    {
        float h = initialHeight;

        heights[0, 0] = Random.value;
        heights[width-1, 0] = Random.value;
        heights[0, height-1] = Random.value;
        heights[width-1, height-1] = Random.value;

        // DiamondSquareStep(0, 0, width-1, height-1, h);
        DiamondSquareStep(width-1, h);
    }

    // EXEMPLO DE CÓDIGO RECURSIVO QUE GERA PROBLEMAS NAS BORDAS DAS CÉLULAS
    //
    // void DiamondSquareStep(int minX, int minY, int maxX, int maxY, float h)
    // {
    //     int dx = maxX - minX;
    //     int dy = maxY - minY;
    //     int area = dx * dy;
    //     if(area > 1)
    //     {
    //         int ci = (minX + maxX) / 2;
    //         int cj = (minY + maxY) / 2;
    //         heights[ci, cj] = (heights[minX, minY] + heights[minX, maxY] + heights[maxX, minY] + heights[maxX, maxY]) / 4 + Random.Range(-h/2, h/2);
            
    //         heights[minX, cj] = (heights[minX, minY] + heights[minX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
    //         heights[ci, minY] = (heights[minX, minY] + heights[maxX, minY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
    //         heights[maxX, cj] = (heights[maxX, minY] + heights[maxX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
    //         heights[ci, maxY] = (heights[minX, maxY] + heights[maxX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);

    //         h *= Mathf.Pow(2, -roughness);

    //         DiamondSquareStep(minX, minY, ci, cj, h);
    //         DiamondSquareStep(ci, minY, maxX, cj, h);
    //         DiamondSquareStep(minX, cj, ci, maxY, h);
    //         DiamondSquareStep(ci, cj, maxX, maxY, h);
    //     }
    // }

    void DiamondSquareStep(int size, float h)
    {
        if(size > 1)
        {
            for(int i = 0; i < width-1; i += size)
            {
                for(int j = 0; j < height-1; j += size)
                {
                    int minX = i;
                    int minY = j;
                    int maxX = minX + size;
                    int maxY = minY + size;

                    // passo do diamante
                    int ci = (minX + maxX) / 2;
                    int cj = (minY + maxY) / 2;
                    heights[ci, cj] = (heights[minX, minY] + heights[minX, maxY] + heights[maxX, minY] + heights[maxX, maxY]) / 4 + Random.Range(-h/2, h/2);
                }
            }

            for(int i = 0; i < width-1; i += size)
            {
                for(int j = 0; j < height-1; j += size)
                {
                    int minX = i;
                    int minY = j;
                    int maxX = minX + size;
                    int maxY = minY + size;
                    int ci = (minX + maxX) / 2;
                    int cj = (minY + maxY) / 2;

                    // passo do quadrado (4 vizinhos com bordas periódicas)
                    // heights[minX, cj] = (heights[minX, minY] + heights[minX, maxY] + heights[ci, cj] + heights[(ci-size) < 0 ? width+(ci-size) : (ci-size), cj]) / 4 + Random.Range(-h/2, h/2);
                    // heights[ci, minY] = (heights[minX, minY] + heights[maxX, minY] + heights[ci, cj] + heights[ci, (cj-size) < 0 ? height+(cj-size) : (cj-size)]) / 4 + Random.Range(-h/2, h/2);
                    // heights[maxX, cj] = (heights[maxX, minY] + heights[maxX, maxY] + heights[ci, cj] + heights[(ci+size)%width, cj]) / 4 + Random.Range(-h/2, h/2);
                    // heights[ci, maxY] = (heights[minX, maxY] + heights[maxX, maxY] + heights[ci, cj] + heights[ci, (cj+size)%height]) / 4 + Random.Range(-h/2, h/2);

                    // passo do quadrado (3 vizinhos)
                    heights[minX, cj] = (heights[minX, minY] + heights[minX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
                    heights[ci, minY] = (heights[minX, minY] + heights[maxX, minY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
                    heights[maxX, cj] = (heights[maxX, minY] + heights[maxX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
                    heights[ci, maxY] = (heights[minX, maxY] + heights[maxX, maxY] + heights[ci, cj]) / 3 + Random.Range(-h/2, h/2);
                }
            }

            h *= Mathf.Pow(2, -roughness);

            DiamondSquareStep(size / 2, h);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        width = terrain.terrainData.heightmapResolution;
        height = terrain.terrainData.heightmapResolution;
        Debug.Log(width + ", " + height);
        heights = new float[width, height];

        switch(method)
        {
            case LandscapeGenerationMethod.ValueNoiseRaw: ValueNoise(); break;
            case LandscapeGenerationMethod.ValueNoiseBilinear: ValueNoise(1); break;
            case LandscapeGenerationMethod.ValueNoiseBicubic: ValueNoise(2); break;
            case LandscapeGenerationMethod.PerlinNoise: PerlinNoise(); break;
            case LandscapeGenerationMethod.FaultFormation: FaultFormation(); break;
            case LandscapeGenerationMethod.DiamondSquare: DiamondSquare(); break;
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
