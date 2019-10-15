using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        if(scale <= 0) //if the scale is less than 1, put it as a default value of 0.0001f
            scale = 0.0001f;
        
        for (int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                //calculate the perlin value for the current x,y
                noiseMap[x, y] = Mathf.PerlinNoise(x / scale, y / scale); ;
            } 
        }//end of for-loops
        return noiseMap;
    } //end of GenerateNoiseMap method
}
