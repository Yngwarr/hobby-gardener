using UnityEngine.Events;

public static class GameEvents
{
    public static readonly UnityEvent SoilClicked = new();
    public static readonly UnityEvent DayPassed = new();
    public static readonly UnityEvent PlantSpawned = new();
}