using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap, ColourMap, MeshMap
    };
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;

    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public bool autoUpdate;
   
    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;

    public float meshHeightScale;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance,lacunarity, offset);

        Color[] colours = new Color[mapWidth * mapHeight];
        for(int y = 0; y<mapHeight; y++)
        {
            for(int x=0; x<mapWidth; x++)
            {
                float currHeight = noiseMap[x, y];
                for(int r=0; r < regions.Length; r++)
                {
                    if(currHeight <= regions[r].height)
                    {
                        colours[y * mapWidth + x] = regions[r].colour;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if(drawMode == DrawMode.ColourMap)
            display.DrawTexture(TextureGenerator.TextureFromColours(colours, mapWidth, mapHeight));
        else if (drawMode == DrawMode.MeshMap)
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightScale), TextureGenerator.TextureFromColours(colours, mapWidth, mapHeight));
    }

    //called automatically when a script variable is changed
    private void OnValidate()
    {
        if (mapWidth < 1)
            mapWidth = 1;
        if (mapHeight < 1)
            mapHeight = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
