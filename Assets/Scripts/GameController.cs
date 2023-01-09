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
    
    [Header("UI")]
    [SerializeField] WeatherPanel[] forecastPanels;
    
    int _ticksPassed;

    void Awake() {
        state.Reset();
        GameEvents.SoilClicked.AddListener(OnSoilClicked);
    }

    void Start() {
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
        UpdateForecast();
        UpdateForecastUI();
    }

    void Update() {
        if (Input.GetButtonUp("Jump")) {
            SkipDay();
        }
    }
    
    void OnSoilClicked() {
        Tick();
    }
    
    public void SkipDay() {
        Tick(true);
    }

    void Tick(bool skipDay = false) {
        _ticksPassed++;
        // TODO remove this when the player gets to choose the seed
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
        
        if (_ticksPassed < TicksPerDay && !skipDay) return;
        
        _ticksPassed = 0;
        gardenBed.DayTick(state.forecast.Peek());
        
        state.forecast.Dequeue();
        UpdateForecast();
        UpdateForecastUI();
        
        state.currentDay++;
        GameEvents.DayPassed.Invoke();
    }
    
    void UpdateForecast() {
        while (state.forecast.Count < ForecastLength) {
            for (var i = 0; i < Random.Range(1, 5); ++i) {
                state.forecast.Enqueue(Random.Range(0, 4) switch {
                    0 => Weather.Cloudy,
                    1 => Weather.Rain,
                    2 => Weather.Sunny,
                    _ => Random.Range(0, 4) switch {
                        0 => Weather.WindN,
                        1 => Weather.WindS,
                        2 => Weather.WindE,
                        _ => Weather.WindW
                    }
                });
            }
        }
        
        // state.forecast.Enqueue(Weather.Sunny);
        
        Debug.Log($"{state.forecast.Peek()}");
    }
    
    void UpdateForecastUI() {
        var i = 0;
        foreach (var w in state.forecast) {
            if (i >= forecastPanels.Length) break;
            forecastPanels[i].SetWeather(w);
            ++i;
        }
    }
}