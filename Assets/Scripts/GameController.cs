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
    
    int _ticksPassed;

    void Awake() {
        GameEvents.SoilClicked.AddListener(OnSoilClicked);
    }

    void Start() {
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
        UpdateForecast();
    }

    void Update() {
        if (Input.GetButtonUp("Jump")) {
            Tick(true);
        }
    }
    
    void OnSoilClicked() {
        Tick();
    }

    void Tick(bool skipDay = false) {
        _ticksPassed++;
        // TODO remove this when the player gets to choose the seed
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
        
        if (_ticksPassed < 2 && !skipDay) return;
        
        _ticksPassed = 0;
        gardenBed.DayTick(state.forecast.Peek());
        
        state.forecast.Dequeue();
        UpdateForecast();
        
        state.currentDay++;
        GameEvents.DayPassed.Invoke();
    }
    
    void UpdateForecast() {
        while (state.forecast.Count < ForecastLength) {
            // TODO make something more predictable
            state.forecast.Enqueue(Tools.RandomWeather());
        }
        Debug.Log($"{state.forecast.Peek()}");
    }
}