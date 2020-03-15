using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public enum DrawMode {
        NoiseMap,
        ColourMap,
        Mesh,
    };

    public DrawMode drawMode;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public float noiseScale = 0.3f;
    public float meshHeightMultiplier;
    public int octaves = 4;
    [Range(0, 1)] public float persistance = 0.5f;
    public float lacunarity = 2f;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate = true;

    public TerrainType[] regions;

    public void GenerateMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        var colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                var currentHeight = noiseMap[x, y];
                for (int region = 0; region < regions.Length; region++) {
                    if (currentHeight < regions[region].height) {
                        colourMap[y * mapWidth + x] = regions[region].colour;
                        break;
                    }
                }
            }
        }

        var display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.ColourMap) {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.NoiseMap){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.Mesh) {
            Debug.Log("Drawing mesh");
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
    }

    void OnValidate() {
        mapWidth = Math.Max(1, mapWidth);
        mapHeight = Math.Max(1, mapHeight);
        lacunarity = Math.Max(1, lacunarity);
        octaves = Math.Max(0, octaves);
    }
}

[Serializable]
public struct TerrainType {
    public string type;
    public float height;
    public Color colour;
}