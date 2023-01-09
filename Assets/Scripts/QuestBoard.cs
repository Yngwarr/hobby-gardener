using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] QuestPanel[] panels;

    void Start() {
        Redraw();
        GameEvents.PlantHarvested.AddListener(PlantHarvested);
    }
    
    void Redraw() {
        for (var i = 0; i < panels.Length; ++i) {
            panels[i].Set(gameState.quests[i]);
        }
    }
    
    void PlantHarvested(PlantInfo info) {
        for (var i = 0; i < gameState.quests.Count; ++i) {
            if (gameState.quests[i].plant != info) {
                Debug.Log($"{gameState.quests[i].plant.name} != {info.name}");
                continue;
            }
            
            gameState.quests[i].targetAmount--;
            
            if (gameState.quests[i].targetAmount != 0) break;
            
            gameState.plants[Random.Range(0, gameState.plants.Length)].seedsLeft += Random.Range(2, 6);
            GameEvents.SeedAmountChanged.Invoke();
            
            gameState.quests[i] = new Quest(gameState.plants);
        }
        
        Redraw();
    }
}