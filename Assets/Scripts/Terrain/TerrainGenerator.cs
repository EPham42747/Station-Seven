using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {
    public Vector2Int dimensions;
    public float frequency;
    public float amplitude;
    public float flatThreshold;
    public float sharpness;
    public float meanHeight;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Start() {
        GetComponent<MeshFilter>().mesh = GenerateMesh();
    }

    private void Update() {
        GetComponent<MeshFilter>().mesh = GenerateMesh();
    }

    private Mesh GenerateMesh() {
        // Create vertices
        vertices = new Vector3[(dimensions.x + 1) * (dimensions.y + 1)];
        for (int i = 0, z = 0; z < dimensions.y + 1; z++) {
            for (int x = 0; x < dimensions.x + 1; x++, i++) {
                float perlinSq = Mathf.Pow(Mathf.PerlinNoise(x / frequency, z / frequency), sharpness);
                float y = perlinSq * amplitude;
                meanHeight += y;
                y = Math.Max(y, flatThreshold);
                vertices[i] = new Vector3(x, y, z);
            }
        }
        meanHeight /= (dimensions.x + 1) * (dimensions.y + 1);

        // Create triangles
        triangles = new int[dimensions.x * dimensions.y * 6];
        for (int z = 0, v = 0, t = 0; z < dimensions.y; z++, v++) {
            for (int x = 0; x < dimensions.x; x++, v++, t += 6) {
                triangles[t + 0] = v;
                triangles[t + 1] = v + dimensions.x + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + dimensions.x + 1;
                triangles[t + 5] = v + dimensions.x + 2;
            }
        }

        // Create mesh and return
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
