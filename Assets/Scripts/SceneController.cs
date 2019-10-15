using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public MapGenerator mapGenerator;
    // Start is called before the first frame update
    void Start()
    {
        mapGenerator.GenerateMap();
    }


}
