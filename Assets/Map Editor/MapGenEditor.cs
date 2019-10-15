using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (MapGenerator))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mg = (MapGenerator)target;
        if(DrawDefaultInspector())
        {
            if (mg.autoUpdate)
                mg.GenerateMap();
        }
        if (GUILayout.Button("Generate"))
        {
            mg.GenerateMap();
        }
    }
}
