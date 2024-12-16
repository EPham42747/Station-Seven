using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    private List<ResourceBuilding> buildings = new List<ResourceBuilding>();

    [Header("Stats")]
    public int startingEnergyCapacity;
    private int maxEnergy;
    private int curEnergy = 0;
    private int energyGain = 0;
    private int energyLoss = 0;

    public int startingOxygenCapacity;
    private int maxOxygen;
    private int curOxygen = 0;
    private int oxygenGain = 0;
    private int oxygenLoss = 0;

    public int startingFoodCapacity;
    private int maxFood;
    private int curFood = 0;
    private int foodGain = 0;
    private int foodLoss = 0;

    public int goalPopulation;
    public float resourcesPerPerson;
    private int curPopulation;

    [Header("Special Buildings")]
    public int energyStorageCapacity;
    public int oxygenStorageCapacity;
    public int foodStorageCapacity;

    [Header("UI")]
    public TMP_Text energyText;
    public TMP_Text energyGainText;
    public TMP_Text energyLossText;

    public TMP_Text oxygenText;
    public TMP_Text oxygenGainText;
    public TMP_Text oxygenLossText;

    public TMP_Text foodText;
    public TMP_Text foodGainText;
    public TMP_Text foodLossText;

    public TMP_Text populationText;

    [Header("Deficit")]
    public int maxDeficitTurns;
    private int deficit = 0;

    public void Start() {
        maxEnergy = startingEnergyCapacity;
        maxOxygen = startingOxygenCapacity;
        maxFood = startingFoodCapacity;
        UpdateText();
    }

    public void UpdateResources() {
        curEnergy += energyGain - energyLoss;
        curOxygen += oxygenGain - oxygenLoss;
        curFood += foodGain - foodLoss;

        curEnergy = Math.Min(curEnergy, maxEnergy);
        curOxygen = Math.Min(curOxygen, maxOxygen);
        curFood = Math.Min(curFood, maxFood);

        energyGain = 0;
        energyLoss = 0;
        oxygenGain = 0;
        oxygenLoss = 0;
        foodGain = 0;
        foodLoss = 0;

        foreach (ResourceBuilding building in buildings) {
            energyGain += building.energyProduction;
            energyLoss += building.energyConsumption;
            oxygenGain += building.oxygenProduction;
            oxygenLoss += building.oxygenConsumption;
            foodGain += building.foodProduction;
            foodLoss += building.foodConsumption;
        }
        
        deficit = curEnergy < 0f || curOxygen < 0f || curFood < 0f ? deficit + 1 : 0;
        
        UpdateText();
    }

    public void UpdateText() {
        energyText.text = $"{curEnergy}/{maxEnergy}";
        oxygenText.text = $"{curOxygen}/{maxOxygen}";
        foodText.text = $"{curFood}/{maxFood}";

        energyText.color = curEnergy < 0 ? Color.red : Color.white;
        oxygenText.color = curOxygen < 0 ? Color.red : Color.white;
        foodText.color = curFood < 0 ? Color.red : Color.white;

        energyGainText.text = $"+{energyGain}";
        oxygenGainText.text = $"+{oxygenGain}";
        foodGainText.text = $"+{foodGain}";

        energyLossText.text = $"-{energyLoss}";
        oxygenLossText.text = $"-{oxygenLoss}";
        foodLossText.text = $"-{foodLoss}";

        populationText.text = $"Maximum capacity: {(int) (Mathf.Min(energyGain - energyLoss, oxygenGain - oxygenLoss, foodGain - foodLoss) / resourcesPerPerson)}/{goalPopulation}";
    }

    public bool TooLongAtDeficit() { return deficit > maxDeficitTurns; }

    public void AddBuilding(ResourceBuilding building) {
        buildings.Add(building);
        energyLoss += building.energyPlaceCost;

        if (building.gameObject.GetComponent<PlaceableObject>().name == "Power Storage") maxEnergy += energyStorageCapacity;
        else if (building.gameObject.GetComponent<PlaceableObject>().name == "O2 Storage") maxOxygen += oxygenStorageCapacity;
        else if (building.gameObject.GetComponent<PlaceableObject>().name == "Food Storage") maxFood += foodStorageCapacity;

        UpdateText();
    }

    public bool HasWon() { return Mathf.Min(energyGain - energyLoss, oxygenGain - oxygenLoss, foodGain - foodLoss) / resourcesPerPerson > goalPopulation; }
}
