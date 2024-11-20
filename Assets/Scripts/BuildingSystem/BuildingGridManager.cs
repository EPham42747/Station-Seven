using UnityEngine;

public class BuildingGridManager : MonoBehaviour {
    public TerrainGenerator terrainGenerator;

    private bool[,] buildableTiles;
    private Vector2Int dimensions;
    private float flatHeight;

    [Header("Rendering")]
    public Transform gridParent;
    public GameObject activeTile;
    public GameObject inactiveTile;

    private void Start() {
        dimensions = terrainGenerator.dimensions;
        flatHeight = terrainGenerator.flatThreshold;

        GenerateGrid();
        RenderGrid();
    }

    private void Update() {
        
    }

    private void GenerateGrid() {
        Vector3[] terrainVertices = terrainGenerator.GetVertices();

        int rows = dimensions.x, cols = dimensions.y;
        buildableTiles = new bool[rows, cols];

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                int bottomLeft = c * (rows + 1) + r;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + rows + 1;
                int topRight = topLeft + 1;

                float yBL = terrainVertices[bottomLeft].y;
                float yBR = terrainVertices[bottomRight].y;
                float yTL = terrainVertices[topLeft].y;
                float yTR = terrainVertices[topRight].y;

                buildableTiles[r, c] = yBL == flatHeight && yBR == flatHeight && yTL == flatHeight && yTR == flatHeight;
            }
        }
    }

    private void RenderGrid() {
        for (int r = 0; r < dimensions.x; r++) {
            for (int c = 0; c < dimensions.y; c++) {
                GameObject tile = Instantiate(buildableTiles[r, c] ? activeTile : inactiveTile, gridParent);
                tile.transform.position = new Vector3(r + 0.5f, flatHeight + 0.01f, c + 0.5f);
            }
        }
    }
}
