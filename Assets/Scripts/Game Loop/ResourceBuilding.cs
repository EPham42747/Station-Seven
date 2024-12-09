using UnityEngine;

public class ResourceBuilding : MonoBehaviour {
    [Header("Placement")]
    public int energyPlaceCost;
    public int oxygenPlaceCost;
    public int foodPlaceCost;

    [Header("Production")]
    public int energyProduction;
    public int oxygenProduction;
    public int foodProduction;

    [Header("Consumption")]
    public int energyConsumption;
    public int oxygenConsumption;
    public int foodConsumption;
}
