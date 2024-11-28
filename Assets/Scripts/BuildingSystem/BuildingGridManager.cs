using System.Collections.Generic;
using UnityEngine;

public class BuildingGridManager : MonoBehaviour {
    public TerrainGenerator terrainGenerator;
    public float offsetHeight;

    private bool[,] buildableTiles;
    private Vector2Int dimensions;
    private float flatHeight;

    [Header("Buildings")]
    public Transform buildingsParent;
    public PlaceableObject targetBuilding;

    private List<(PlaceableObject, Vector2Int)> buildings = new List<(PlaceableObject, Vector2Int)>();

    [Header("Rendering")]
    public Transform gridParent;
    public GameObject activeTile;
    public GameObject inactiveTile;

    private GameObject indicator;

    private void Start() {
        dimensions = terrainGenerator.dimensions;
        flatHeight = terrainGenerator.flatThreshold;

        GenerateGrid();
        RenderGrid();

        indicator = Instantiate(targetBuilding.prefab);
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Vector2Int gridPos = new Vector2Int((int) Mathf.Floor(hit.point.x), (int) Mathf.Floor(hit.point.z));
            
            if (!indicator) indicator = Instantiate(targetBuilding.prefab);
            indicator.transform.position = new Vector3(gridPos.x, terrainGenerator.flatThreshold + offsetHeight, gridPos.y);

            if (Input.GetMouseButtonDown(0) && Placeable(gridPos, targetBuilding.size)) Place(targetBuilding, gridPos);
        }
        else Destroy(indicator);
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
        for (int i = 0; i < gridParent.childCount - 1; i++) Destroy(gridParent.GetChild(i).gameObject);

        for (int r = 0; r < dimensions.x; r++) {
            for (int c = 0; c < dimensions.y; c++) {
                GameObject tile = Instantiate(buildableTiles[r, c] ? activeTile : inactiveTile, gridParent);
                tile.transform.position = new Vector3(r + 0.5f, flatHeight + offsetHeight, c + 0.5f);
            }
        }
    }

    public bool Placeable(Vector2Int position, Vector2Int size) {
        if (position.x < 0 || position.y < 0 || position.x + size.x > dimensions.x || position.y + size.y > dimensions.y) return false;

        for (int r = position.x; r < position.x + size.x; r++) {
            for (int c = position.y; c < position.y + size.y; c++) {
                if (!buildableTiles[r, c]) return false;
            }
        }
        return true;
    }

    public void Place(PlaceableObject placeable, Vector2Int position) {
        for (int r = position.x; r < position.x + placeable.size.x; r++) {
            for (int c = position.y; c < position.y + placeable.size.y; c++) {
                if (!buildableTiles[r, c]) {
                    Debug.LogError($"Invalid building tile ({r}, {c})");
                    return;
                }
                buildableTiles[r, c] = false;
            }
        }
        buildings.Add((placeable, position));

        GameObject o = Instantiate(placeable.prefab, buildingsParent);
        o.transform.position = new Vector3(position.x, flatHeight + offsetHeight, position.y);
        RenderGrid();
    }

    public void SetTargetBuilding(PlaceableObject placeable) { targetBuilding = placeable; }
}
