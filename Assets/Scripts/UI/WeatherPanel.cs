using System;
using UnityEngine;

public class WeatherPanel : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject rain;
    [SerializeField] GameObject sun;
    [SerializeField] GameObject wind;
    
    [Header("Arrows")]
    [SerializeField] GameObject up;
    [SerializeField] GameObject down;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    
    Weather _current;
    
    public void SetWeather(Weather weather) {
        if (_current == weather) return;
        
        GetIcon(_current).SetActive(false);
        GetIcon(weather).SetActive(true);
        
        if ((_current & Weather.Windy) != 0) {
            GetWindIcon(_current).SetActive(false);
        }
        if ((weather & Weather.Windy) != 0) {
            GetWindIcon(weather).SetActive(true);
        }
        
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
    
    GameObject GetWindIcon(Weather weather) {
        return weather switch {
            Weather.WindN => down,
            Weather.WindS => up,
            Weather.WindE => left,
            Weather.WindW => right,
            _ => null
        };
    }
}