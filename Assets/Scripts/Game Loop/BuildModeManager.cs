using UnityEngine;

public class BuildModeManager : MonoBehaviour {
    public BuildingGridManager buildingGridManager;

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void SetTargetBuilding(PlaceableObject placeable) { buildingGridManager.SetTargetBuilding(placeable); }
}
