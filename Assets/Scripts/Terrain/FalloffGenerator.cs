using UnityEngine;

public class FalloffGenerator {
    public static float[,] Generate(int width, int height, float strength) {
        float[,] ret = new float[width, height];

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                float _i = Mathf.Abs(i / (float) width - 0.5f) * 2;
                float _j = Mathf.Abs(j / (float) height - 0.5f) * 2;

                float value = Mathf.Pow(Mathf.Max(_i, _j), strength);
                ret[i, j] = value;
            }
        }

        return ret;
    }
}
