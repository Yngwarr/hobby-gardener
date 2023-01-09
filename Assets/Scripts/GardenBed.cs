using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    const int W = 8;
    const int H = 6;
    const float Step = 3.5f;
    
    [SerializeField] GameObject soilPrefab;
    
    Soil[] _soils = new Soil[W * H];
    
    void Start() {
        for (var x = 0; x < W; ++x) {
            for (var y = 0; y < H; ++y) {
                var go = Instantiate(soilPrefab,
                    new Vector3(x * Step, 0, y * Step),
                    Quaternion.identity,
                    transform
                );
                var soil = go.GetComponent<Soil>();
                soil.gridPos = new Vector2Int(x, y);
                SetSoil(x, y, soil);
            }
        }
    }
    
    public void DayTick(Weather weather) {
        var bamboos = new Queue<Soil>();
        var berries = new Queue<Soil>();
        
        foreach (var soil in _soils) {
            if (soil == null) continue;
            if (soil.plant == null) continue;
            
            switch (soil.plant) {
                case Bamboo:
                    bamboos.Enqueue(soil);
                    break;
                case Lightberry:
                    berries.Enqueue(soil);
                    break;
            }
        }

        foreach (var soil in bamboos) soil.DayTick(weather, GetNeighbor);
        foreach (var soil in berries) soil.DayTick(weather, GetNeighbor);
    }
    
    void SetSoil(int x, int y, Soil soil) {
        _soils[y + x * H] = soil;
    }
    
    Soil GetNeighbor(Vector2Int pos, int xOffset, int yOffset) {
        return GetSoil(pos.x + xOffset, pos.y + yOffset);
    }
    
    Soil GetSoil(int x, int y) {
        if (x < 0 || x >= W) return null;
        if (y < 0 || y >= H) return null;
        
        return _soils[y + x * H];
    }
}
