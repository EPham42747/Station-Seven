using UnityEngine;

public static class NoiseGenerator {
    public static float[,] Generate(int width, int height, float frequency = 1f, float sharpness = 1f, float falloffStrength = 1f) {
        if (frequency <= 0f) frequency = 0.001f;
        if (sharpness <= 0f) sharpness = 0.001f;

        float offset = 0f;
        float[,] ret = new float[width, height];
        float[,] falloff = FalloffGenerator.Generate(width, height, falloffStrength);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // Fill with Perlin value
                ret[i, j] = Mathf.PerlinNoise(i * frequency + offset, j * frequency + offset);
                // Subtract falloff
                ret[i, j] -= falloff[i, j];
                // Include sharpness
                ret[i, j] = Mathf.Pow(ret[i, j], sharpness);
            }
        }
        
        return ret;
    }
}
