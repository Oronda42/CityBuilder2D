using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int sizeX, int sizeY,float scale)
    {

        if (scale <= 0)
            scale = 0.0001f;
        
        float[,] noiseMap = new float[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                //float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                //noiseMap[x, y] = perlinValue;

                noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                 
            }
        }

        return noiseMap;
    }
}

