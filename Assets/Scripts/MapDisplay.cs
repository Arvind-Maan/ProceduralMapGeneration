using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer; 
    public void DrawMap(float[,] map)
    {
        int w = map.GetLength(0); //the width
        int h = map.GetLength(1); //the height
        //create a texture of the maps width and height
        Texture2D texture = new Texture2D(w,h);

        //generate an array of all of the colours for the pixels
        Color[] colours = new Color[w * h];
        for(int y = 0; y< h; y++)
        {
            for(int x = 0; x<w; x++)
            {
                //y*W (current row we are) x is the current index
                //so, y*w+x, is our current index
                colours[y * w + x] = Color.Lerp(Color.black, Color.white, map[x, y]);
            }
        }
        texture.SetPixels(colours);
        texture.Apply();
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(w, 1, h);
    }
}
