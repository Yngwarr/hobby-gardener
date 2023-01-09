using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Quest
{
    public PlantInfo plant;
    public int targetAmount;
    
    public Quest(IReadOnlyList<PlantInfo> plants) {
        targetAmount = Mathf.FloorToInt(Random.value * Random.value / 2 * 10 + 1);
        plant = plants[Random.Range(0, plants.Count)];
    }
    
    public override string ToString() {
        return $"{targetAmount} of {plant.name}";
    }
}