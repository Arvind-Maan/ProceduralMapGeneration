using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator 
{
    public static Texture2D TextureFromColours(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(colourMap);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int w = heightMap.GetLength(0); //the width
        int h = heightMap.GetLength(1); //the height
        //create a texture of the maps width and height
        Texture2D texture = new Texture2D(w, h);

        //generate an array of all of the colours for the pixels
        Color[] colours = new Color[w * h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                //y*W (current row we are) x is the current index
                //so, y*w+x, is our current index
                colours[y * w + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColours(colours, w, h);
    }

}
