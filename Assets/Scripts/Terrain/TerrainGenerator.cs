using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {
    [Header("Mesh")]
    public Vector2Int dimensions;
    public float scale;
    public float frequency;
    public float amplitude;
    public float flatThreshold;
    public float sharpness;
    private Vector3[] vertices;
    private int[] triangles;

    private float[,] noiseMap;

    private void Start() {
        
    }

    private void Update() {
        noiseMap = NoiseGenerator.GenerateNoise(dimensions.x + 1, dimensions.y + 1, frequency, amplitude, flatThreshold, sharpness);
        GetComponent<MeshFilter>().mesh = MakeFlatShaded(GenerateMesh());
    }

    private Mesh GenerateMesh() {
        // Create vertices
        vertices = new Vector3[(dimensions.x + 1) * (dimensions.y + 1)];
        for (int z = 0, i = 0; z < dimensions.y + 1; z++) {
            for (int x = 0; x < dimensions.x + 1; x++, i++)
                vertices[i] = new Vector3(x * scale, noiseMap[x, z], z * scale);
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

        // Create mesh and return
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    private Mesh MakeFlatShaded(Mesh mesh) {
        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;

        Vector3[] newVertices = new Vector3[oldTriangles.Length];
        int[] newTriangles = new int[oldTriangles.Length];

        for (int i = 0; i < oldTriangles.Length; i++) {
            // Get positions of old vertices and assign to new
            newVertices[i] = oldVertices[oldTriangles[i]];
            newTriangles[i] = i;
        }

        Mesh ret = new Mesh();
        ret.vertices = newVertices;
        ret.triangles = newTriangles;
        ret.RecalculateNormals();
        return ret;
    }
}
