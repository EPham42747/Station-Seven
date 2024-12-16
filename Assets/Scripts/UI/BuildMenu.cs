using UnityEngine;

public class BuildMenu : MonoBehaviour {
    public BuildModeManager buildModeManager;
    public PlaySound playSound;

    public void SetActiveBuilding(PlaceableObject placeable) {
        buildModeManager.SetTargetBuilding(placeable);
        playSound.PlayClick();
    }
}
