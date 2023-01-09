using UnityEngine.Events;

public static class GameEvents
{
    public static readonly UnityEvent SoilClicked = new();
    public static readonly UnityEvent DayPassed = new();
    public static readonly UnityEvent SeedAmountChanged = new();
    public static readonly UnityEvent<int> ExpChanged = new();
    public static readonly UnityEvent<PlantInfo> PlantHarvested = new();
}