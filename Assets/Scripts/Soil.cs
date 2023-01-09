using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Soil : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameState gameState;
    [SerializeField] GameObject highlight;
    
    public Vector2 gridPos;
    
    Vector3 _defaultScale;
    public Plant plant { get; private set; }
    
    void Start() {
        _defaultScale = highlight.transform.localScale;
        highlight.transform.localScale = Vector3.zero;
    }
    
    void ShowHighlight() {
        highlight.transform.DOScale(_defaultScale, .2f);
    }
    
    void HideHighlight() {
        highlight.transform.DOScale(0, .2f);
    }

    public void Tick(Func<int, int, Soil> neighbor) {
        if (plant == null) return;
        
        plant.Tick(neighbor);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ShowHighlight();
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideHighlight();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (plant == null) {
            var go = Instantiate(
                gameState.selectedSeed.plantPrefab,
                transform
            );
            plant = go.GetComponent<Plant>();
            plant.Spawn();
        } else {
            plant.Harvest();
        }
        
        GameEvents.SoilClicked.Invoke();
    }
}
