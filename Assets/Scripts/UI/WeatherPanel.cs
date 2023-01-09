using System;
using UnityEngine;

public class WeatherPanel : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject rain;
    [SerializeField] GameObject sun;
    [SerializeField] GameObject wind;
    
    Weather _current;
    
    void Start() {
        cloud.SetActive(false);
        rain.SetActive(false);
        sun.SetActive(false);
        wind.SetActive(false);
    }
    
    public void SetWeather(Weather weather) {
        GetIcon(_current).SetActive(false);
        GetIcon(weather).SetActive(true);
        _current = weather;
    }
    
    GameObject GetIcon(Weather weather) {
        if ((weather & Weather.Windy) != 0) {
            return wind;
        }
        return weather switch {
            Weather.Rain => rain,
            Weather.Sunny => sun,
            _ => cloud
        };
    }
}