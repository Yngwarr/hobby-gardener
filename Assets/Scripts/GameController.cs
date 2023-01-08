using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] GameState state;
    // TODO find a way to populate it from code
    [SerializeField] PlantInfo[] plants;

    void Start() {
        state.selectedSeed = plants[Random.Range(0, plants.Length)];
    }
}