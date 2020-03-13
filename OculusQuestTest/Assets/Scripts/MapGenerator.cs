using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int mapWidth = 10;
    public int mapHeight = 10;
    public float noiseScale = 0.3f;
    public bool autoUpdate = true;

    public void GenerateNoiseMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}