using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        if(scale <= 0) //if the scale is less than 1, put it as a default value of 0.0001f
            scale = 0.0001f;

        float maxNHeight = float.MinValue;
        float minNHeight = float.MaxValue;
        for (int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1, frequency = 1, noiseHeight = 0;
                for (int o = 0; o < octaves; o++)
                {
                    //calculate the current x and y values (the higher the freq, the further the points)
                    float x_coord = x / scale * frequency;
                    float y_coord = y / scale * frequency;

                    //calculate the perlin value for the current x,y
                    float perlVal = Mathf.PerlinNoise(x_coord,y_coord) * 2 - 1; // (*2-1 allows for negative values)

                    //increase the noise height by perlin val of each octave
                    noiseHeight += perlVal * amplitude;
                    amplitude *= persistance; //decreases each octave
                    frequency *= lacunarity; //increases
                } //end of octaves loop
                noiseMap[x, y] = noiseHeight;
                //the range of our noise height values.
                if (noiseHeight > maxNHeight)
                    maxNHeight = noiseHeight;
                else if (noiseHeight < minNHeight)
                    minNHeight = noiseHeight;
            } //end of x for loop
        }//end of for-loops

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //if the value is equal to the minNHeight. it will return 0.
                //if the value is equal to maxNHeight it will return 1.
                //all values in between will be a fractions. This helps normalize our range.
                noiseMap[x, y] = Mathf.InverseLerp(minNHeight, maxNHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    } //end of GenerateNoiseMap method
}
