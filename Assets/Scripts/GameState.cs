using System;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public PlantInfo selectedSeed = null;

    void Reset() {
        selectedSeed = null;
    }

    void Awake() {
        Reset();
    }
}