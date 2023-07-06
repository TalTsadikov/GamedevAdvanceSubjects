using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [Header("Type of Ground Colors")]
    [SerializeField] Color holeColor;
    [SerializeField] Color landColor;

    [Header("Map Variants")]
    [SerializeField] int size;
    [SerializeField] float scale = 0.1f;
    [SerializeField] float holeLevel=0.4f;
    float _noiseValue;

    Tile[,] grid;
    float[,] noiseMap;

    private void Awake()
    {
        CreatMap();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;
        for(int y=0; y< size; y++) 
        {
            for(int x=0; x<size; x++) 
            {
                Tile tile= grid[y,x];
                if(tile.isHole) 
                {
                    Gizmos.color = new Color(holeColor.r, holeColor.g, holeColor.b,1);
                }
                else
                    Gizmos.color = new Color(landColor.r, landColor.g, landColor.b, 1);

                Vector3 pos = new Vector3(x, 0, y);
                Gizmos.DrawCube(pos, Vector3.one);

            }
        }
    }

    void CreateNoiseMap()
    {
        noiseMap = new float[size, size];
        float xOffset= Random.Range(-10000, 1000);
        float yOffset= Random.Range(-10000, 1000);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                _noiseValue = Mathf.PerlinNoise(x * scale +xOffset , y * scale + yOffset);
                noiseMap[y, x] = _noiseValue;
            }
        }
    }

    void CreateTileMap()
    {
        grid = new Tile[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Tile tile = new Tile();
                float noiseValue = noiseMap[y, x];
                tile.isHole = noiseValue < holeLevel;
                grid[y, x] = tile;
            }
        }
    }

    void CreatMap()
    {
        CreateNoiseMap();
        CreateTileMap();
    }

    public void CreateNew()
    {
        CreatMap();
    }
}
