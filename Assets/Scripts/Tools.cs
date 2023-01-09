using UnityEngine;

public static class Tools
{
    public static Weather RandomWeather() {
        return (Weather)(1 << Random.Range(0, 6));
    }
}