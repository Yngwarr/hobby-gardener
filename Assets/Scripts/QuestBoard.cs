using System;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] QuestPanel[] panels;

    void Start() {
        for (var i = 0; i < panels.Length; ++i) {
            panels[i].Set(gameState.quests[i]);
        }
    }
}