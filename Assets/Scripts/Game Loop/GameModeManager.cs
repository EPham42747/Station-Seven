using UnityEngine;

public class GameModeManager : MonoBehaviour {
    [Header("Play")]
    public PlayerController playerController;

    [Header("Build")]
    public BuildingGridManager buildingGridManager;
    public GameObject buildMenu;

    private void Start() {
        SetPlayMode();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) SetPlayMode();
        else if (Input.GetKeyDown(KeyCode.B)) SetBuildMode();
    }

    private void SetPlayMode() {
        Reset();

        playerController.enabled = true;
    }

    private void SetBuildMode() {
        Reset();

        buildingGridManager.enabled = true;
        buildMenu.SetActive(true);
    }

    private void Reset() {
        playerController.enabled = false;

        if (buildingGridManager.enabled) buildingGridManager.Disable();
        buildMenu.SetActive(false);
    }

    public void Disable() {
        SetPlayMode();
        this.enabled = false;
    }
}
