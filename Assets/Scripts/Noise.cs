﻿using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        if(scale <= 0) scale = 0.0001f;

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            var offsetX = prng.Next(-100000, 100000) + offset.x;
            var offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
            
        float[,] noiseMap = new float[mapWidth, mapHeight];
        var maxNoiseHeight = float.MinValue;
        var minNoiseHeight = float.MaxValue;
        var halfHeight = mapHeight / 2f;
        var halfWidth = mapWidth / 2f;
        
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;
                
                    // Making perlin noise returning also negative values
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;    
                    
                    
                    if(noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                    else if(noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;
                    noiseMap[x, y] = noiseHeight;
                }
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }

        return noiseMap;
    }
}
