using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public enum DrawMode {
        NoiseMap,
        ColourMap,
        Mesh,
    };

    public DrawMode drawMode;

    public const int mapChunkSize = 241;
    [Range(0, 6)] public int levelOfDetail;

    public float noiseScale = 0.3f;
    public int octaves = 4;
    [Range(0, 1)] public float persistance = 0.5f;
    public float lacunarity = 2f;
    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate = true;

    public TerrainType[] regions;

    public void GenerateMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        var colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++) {
            for (int x = 0; x < mapChunkSize; x++) {
                var currentHeight = noiseMap[x, y];
                for (int region = 0; region < regions.Length; region++) {
                    if (currentHeight < regions[region].height) {
                        colourMap[y * mapChunkSize + x] = regions[region].colour;
                        break;
                    }
                }
            }
        }

        var display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.ColourMap) {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.NoiseMap) {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.Mesh) {
            Debug.Log("Drawing mesh");
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
    }

    void OnValidate() {
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