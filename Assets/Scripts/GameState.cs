using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public PlantInfo[] plants;
    public PlantInfo selectedSeed;
    public int currentDay;
    public Queue<Weather> forecast = new();
    public List<Quest> quests = new();
    
    int _exp;
    public int Exp {
        get => _exp;
        set {
            _exp = value;
            GameEvents.ExpChanged.Invoke(_exp);
        }
    }

    public void Reset() {
        selectedSeed = null;
        forecast.Clear();
        currentDay = 0;
    }
}