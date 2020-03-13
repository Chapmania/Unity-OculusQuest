using System;
using UnityEngine;
using Random = System.Random;

public static class Noise {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        var noiseMap = new float[mapWidth, mapHeight];

        var prng = new Random(seed);
        var octaveOffsets = new Vector2[octaves];
        for (int octave = 0; octave < octaves; octave++) {
            var offsetX = prng.Next(-100000, 100000) + offset.x;
            var offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[octave] = new Vector2(offsetX, offsetY);
        }


        if (scale <= 0) {
            scale = 0.0001f;
        }

        var maxNoiseHeight = float.MinValue;
        var minNoiseHeight = float.MaxValue;
        var halfWidth = mapWidth / 2f;
        var halfHeight = mapHeight / 2f;


        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0;
                for (int octave = 0; octave < octaves; octave++) {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[octave].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[octave].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                minNoiseHeight = Math.Min(minNoiseHeight, noiseHeight);
                maxNoiseHeight = Math.Max(maxNoiseHeight, noiseHeight);
                noiseMap[x, y] = noiseHeight;
            }
        }

        float range = maxNoiseHeight - minNoiseHeight;
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}