using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int mapWidth = 10;
    public int mapHeight = 10;
    public float noiseScale = 0.3f;

    public int octaves = 4;
    [Range(0, 1)]
    public float persistance = 0.5f;
    public float lacunarity = 2f;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate = true;


    public void GenerateNoiseMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate() {
        mapWidth = Math.Max(1, mapWidth);
        mapHeight = Math.Max(1, mapHeight);
        lacunarity = Math.Max(1, lacunarity);
        octaves = Math.Max(0, octaves);
    }
}