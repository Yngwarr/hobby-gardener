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
    
    Vector3 _defaultScale;
    Plant _plant;
    
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

    public void OnPointerEnter(PointerEventData eventData) {
        ShowHighlight();
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideHighlight();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_plant != null) return;
        
        var go = Instantiate(
            gameState.selectedSeed.grownPrefab,
            transform
        );
        _plant = go.GetComponent<Plant>();
    }
}
