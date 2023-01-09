using System;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelector : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] PlantOptionButton[] options;

    void Start() {
        Set(options[0]);
    }
    
    void ResetButtons() {
        foreach (var opt in options) {
            opt.Set(false);
        }
    }
    
    public void Set(PlantOptionButton opt) {
        ResetButtons();
        opt.Set(true);
        gameState.selectedSeed = opt.Info;
    }
}