using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int W = 8;
    const int H = 6;
    const float Step = 3.5f;
    
    [SerializeField] GameObject soilPrefab;
    
    void Start() {
        for (var x = 0; x < W; ++x) {
            for (var y = 0; y < H; ++y) {
                Instantiate(soilPrefab, new Vector3(x * Step, 0, y * Step), Quaternion.identity);
            }
        }
    }
}
