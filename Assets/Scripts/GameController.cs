using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    const int ForecastLength = 7;
    
    [SerializeField] GameState state;
    // TODO find a way to populate it from code
    [SerializeField] PlantInfo[] plants;
    [SerializeField] GardenBed gardenBed;

    void Awake() {
        GameEvents.SoilClicked.AddListener(Tick);
    }

    void Start() {
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
    }
    
    void Tick() {
        gardenBed.Tick();
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
    }
    
    void UpdateForecast() {
        while (state.forecast.Count < ForecastLength) {
            // TODO make something more predictable
            state.forecast.Enqueue(Tools.RandomWeather());
        }
    }
}