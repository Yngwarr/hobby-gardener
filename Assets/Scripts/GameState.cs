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

    public void Reset() {
        selectedSeed = null;
        forecast.Clear();
        currentDay = 0;
    }
}