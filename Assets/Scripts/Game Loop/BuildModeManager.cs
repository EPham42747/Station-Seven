using UnityEngine;

public class BuildModeManager : MonoBehaviour {
    public BuildingGridManager buildingGridManager;
    public ResourceManager resourceManager;
    public TerrainGenerator terrainGenerator;
    public NPCManager npcManager;

    public int npcFrequency;
    private int curNpc = 1;

    public void SetTargetBuilding(PlaceableObject placeable) { buildingGridManager.SetTargetBuilding(placeable); }

    public void UpdateBuildings(ResourceBuilding resourceBuilding, Vector3 npcPosition) {
        resourceManager.AddBuilding(resourceBuilding);
        terrainGenerator.UpdateNavMesh();
        
        if (curNpc >= npcFrequency) {
            npcManager.InstantiateNPC(npcPosition);
            curNpc = 0;
        }
        curNpc++;
    }
}
