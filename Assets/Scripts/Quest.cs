using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Quest
{
    public PlantInfo Plant;
    public int TargetAmount;
    
    public Quest(IReadOnlyList<PlantInfo> plants) {
        TargetAmount = Mathf.FloorToInt(Random.value * Random.value / 2 * 10 + 1);
        Plant = plants[Random.Range(0, plants.Count)];
    }
    
    public override string ToString() {
        return $"{TargetAmount} of {Plant.name}";
    }
}