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
                soil.gridPos = new Vector2(x, y);
                SetSoil(x, y, soil);
            }
        }
    }
    
    public void DayTick() {
        foreach (var soil in _soils) {
            soil.DayTick(GetSoil);
        }
    }
    
    void SetSoil(int x, int y, Soil soil) {
        _soils[y + x * H] = soil;
    }
    
    Soil GetSoil(int x, int y) {
        if (x < 0 || x >= W) return null;
        if (y < 0 || y >= H) return null;
        
        return _soils[y + x * H];
    }
}
