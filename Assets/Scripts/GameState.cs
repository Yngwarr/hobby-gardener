using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public PlantInfo selectedSeed;
    public Queue<Weather> forecast = new();
    public int currentDay;

    void Reset() {
        selectedSeed = null;
        forecast.Clear();
        currentDay = 0;
    }

    void Awake() {
        Reset();
    }
}