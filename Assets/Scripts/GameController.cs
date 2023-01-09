using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    const int ForecastLength = 7;
    const int TicksPerDay = 2;
    
    [SerializeField] GameState state;
    // TODO find a way to populate it from code
    [SerializeField] PlantInfo[] plants;
    [SerializeField] GardenBed gardenBed;
    
    int ticksPassed = 0;

    void Awake() {
        GameEvents.SoilClicked.AddListener(Tick);
    }

    void Start() {
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
    }
    
    void Tick() {
        ticksPassed++;
        // TODO remove this when the player gets to choose the seed
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
        
        if (ticksPassed < 2) return;
        
        ticksPassed = 0;
        gardenBed.DayTick();
        state.currentDay++;
        GameEvents.DayPassed.Invoke();
    }
    
    void UpdateForecast() {
        while (state.forecast.Count < ForecastLength) {
            // TODO make something more predictable
            state.forecast.Enqueue(Tools.RandomWeather());
        }
    }
}