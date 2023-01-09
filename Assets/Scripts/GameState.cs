using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public PlantInfo selectedSeed;
    public Queue<Weather> forecast;

    void Reset() {
        selectedSeed = null;
    }

    void Awake() {
        Reset();
    }
}