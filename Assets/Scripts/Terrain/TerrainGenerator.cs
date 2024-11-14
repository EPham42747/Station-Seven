using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {
    [Header("Mesh")]
    public Vector2Int dimensions;
    public float scale;
    public float frequency;
    public float amplitude;
    public float flatThreshold;
    public float sharpness;
    public float falloffStrength;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    [Header("Color Map")]
    public Gradient gradient;
    public Material material;

    private float[,] noiseMap;

    private void Start() {

    }

    private void Update() {
        noiseMap = NoiseGenerator.Generate(dimensions.x + 1, dimensions.y + 1, frequency, sharpness, falloffStrength);
        GetComponent<MeshFilter>().mesh = MakeFlatShaded(GenerateMesh());

        material.mainTexture = GenerateTexture();
        GetComponent<MeshRenderer>().material = material;
    }

    private Mesh GenerateMesh() {
        // Create vertices
        vertices = new Vector3[(dimensions.x + 1) * (dimensions.y + 1)];
        for (int z = 0, i = 0; z < dimensions.y + 1; z++) {
            for (int x = 0; x < dimensions.x + 1; x++, i++)
                vertices[i] = new Vector3(x * scale, Mathf.Max(noiseMap[x, z] * amplitude, flatThreshold), z * scale);
        }

        // Create triangles
        triangles = new int[dimensions.x * dimensions.y * 6];
        for (int z = 0, v = 0, t = 0; z < dimensions.y; z++, v++) {
            for (int x = 0; x < dimensions.x; x++, v++, t += 6) {
                triangles[t] = v;
                triangles[t + 1] = v + dimensions.x + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + dimensions.x + 1;
                triangles[t + 5] = v + dimensions.x + 2;
            }
        }

        // Create UVs
        uvs = new Vector2[(dimensions.x + 1) * (dimensions.y + 1)];
        for (int z = 0, i = 0; z < dimensions.y + 1; z++) {
            for (int x = 0; x < dimensions.x + 1; x++, i++)
                uvs[i] = new Vector2((float) x / (dimensions.x + 1), (float) z / (dimensions.y + 1));
        }

        // Create mesh and return
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

    private Mesh MakeFlatShaded(Mesh mesh) {
        Vector3[] oldVertices = mesh.vertices;
        Vector2[] oldUVs = mesh.uv;
        int[] oldTriangles = mesh.triangles;

        Vector3[] newVertices = new Vector3[oldTriangles.Length];
        Vector2[] newUVs = new Vector2[oldTriangles.Length];
        int[] newTriangles = new int[oldTriangles.Length];

        for (int i = 0; i < oldTriangles.Length; i++) {
            // Get positions of old vertices and assign to new
            newVertices[i] = oldVertices[oldTriangles[i]];
            newUVs[i] = oldUVs[oldTriangles[i]];
            newTriangles[i] = i;
        }

        Mesh ret = new Mesh();
        ret.vertices = newVertices;
        ret.triangles = newTriangles;
        ret.uv = newUVs;
        ret.RecalculateNormals();
        return ret;
    }

    private Texture2D GenerateTexture() {
        Texture2D ret = new Texture2D(dimensions.x + 1, dimensions.y + 1);
        ret.filterMode = FilterMode.Point;

        for (int i = 0; i < dimensions.x + 1; i++) {
            for (int j = 0; j < dimensions.y + 1; j++) {
                ret.SetPixel(i, j, gradient.Evaluate(noiseMap[i, j]));
            }
        }
        ret.Apply();

        return ret;
    }
}
