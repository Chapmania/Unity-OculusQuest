using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        var mapGen = FindObjectOfType<MapGenerator>();
        mapGen.GenerateMap();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
