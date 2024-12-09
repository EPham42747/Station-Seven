using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    private List<ResourceBuilding> buildings = new List<ResourceBuilding>();

    [Header("Stats")]
    public int maxEnergy;
    private int curEnergy = 0;
    private int energyGain = 0;
    private int energyLoss = 0;

    public int maxOxygen;
    private int curOxygen = 0;
    private int oxygenGain = 0;
    private int oxygenLoss = 0;

    public int maxFood;
    private int curFood = 0;
    private int foodGain = 0;
    private int foodLoss = 0;

    [Header("UI")]
    public TMP_Text energyText;
    public TMP_Text oxygenText;
    public TMP_Text foodText;

    [Header("Deficit")]
    public int maxDeficitTurns;
    private int deficit = 0;

    public void UpdateResources() {
        energyGain = 0;
        energyLoss = 0;
        oxygenGain = 0;
        oxygenLoss = 0;
        foodGain = 0;
        foodLoss = 0;
    
        foreach (ResourceBuilding building in buildings) {
            curEnergy += building.energyProduction - building.energyConsumption;
            curOxygen += building.oxygenProduction - building.oxygenConsumption;
            curFood += building.foodProduction - building.foodConsumption;

            curEnergy = Math.Min(curEnergy, maxEnergy);
            curOxygen = Math.Min(curOxygen, maxOxygen);
            curFood = Math.Min(curFood, maxFood);

            energyGain += building.energyProduction;
            energyLoss += building.energyConsumption;
            oxygenGain += building.oxygenProduction;
            oxygenLoss += building.oxygenConsumption;
            foodGain += building.foodProduction;
            foodLoss += building.foodConsumption;

            if (curEnergy < 0f || curOxygen < 0f || curFood < 0f) deficit++;
            else deficit = 0;

            UpdateText();
        }
    }

    public void UpdateText() {
        energyText.text = $"{curEnergy}/{maxEnergy}";
        oxygenText.text = $"{curOxygen}/{maxOxygen}";
        foodText.text = $"{curFood}/{maxFood}";

        energyText.color = curEnergy < 0 ? Color.red : Color.white;
        oxygenText.color = curOxygen < 0 ? Color.red : Color.white;
        foodText.color = curFood < 0 ? Color.red : Color.white;
    }

    public bool TooLongAtDeficit() { return deficit > maxDeficitTurns; }
    public void AddBuilding(ResourceBuilding building) { buildings.Add(building); }
}
