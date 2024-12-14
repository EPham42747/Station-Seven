using UnityEngine;

public class BuildMenu : MonoBehaviour {
    public BuildModeManager buildModeManager;
    public void SetActiveBuilding(PlaceableObject placeable) { buildModeManager.SetTargetBuilding(placeable); }
}
