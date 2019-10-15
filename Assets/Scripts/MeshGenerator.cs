using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator 
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightScale)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        MeshData mesh = new MeshData(width, height);
        int vertexIndex = 0;
        float topLeft_X = (width - 1) / -2f;
        float topLeft_Z = (height - 1) / 2f;
        for(int y = 0; y< height; y++)
        { 
            for(int x = 0; x< width; x++)
            {
                mesh.vertices[vertexIndex] = new Vector3(topLeft_X + x, heightMap[x, y] * heightScale, topLeft_Z - y);
                mesh.uvs[vertexIndex] = new Vector2(x/((float)width), y/((float)height));
                //setting up the triangles for the mesh
                //ignore right and bottom edge vertices since those will already be calculated by previous index.
                if (x < width - 1 && y < width - 1)
                {
                    mesh.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    mesh.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + width);
                } //end of if
                vertexIndex++;
            } //end of for-loop x
        } //end of for-loop y
        return mesh;
    } //end of method GenerateTerrainMesh
} //end of class MeshGen


public class MeshData
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangles;
    int index;
    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshHeight * meshWidth];
        triangles = new int[6 *(meshWidth - 1) * (meshHeight - 1)];
        uvs = new Vector2[meshWidth * meshHeight];
    } //end of constructor

    public void AddTriangle(int x, int y, int z)
    {
        triangles[index] = x;
        triangles[index+1] = y;
        triangles[index+2] = y;
        index += 3;
    } //end of addTriangle
    
    public Mesh CreateMesh()
    {
        Mesh toRet = new Mesh();
        toRet.vertices = vertices;
        toRet.triangles = triangles;
        toRet.uv = uvs;
        toRet.RecalculateNormals();
        return toRet;
    }
} //end of class MeshData