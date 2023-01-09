using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Soil : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameState gameState;
    [SerializeField] GameObject highlight;
    
    public Vector2Int gridPos;
    
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

    public void DayTick(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if (!plant) return;
        
        plant.DayTick(weather, neighbor);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ShowHighlight();
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideHighlight();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (plant) {
            plant.Harvest();
        } else {
            if (gameState.selectedSeed.seedsLeft <= 0) {
                return;
            }
            Plant(gameState.selectedSeed);
        }

        GameEvents.SoilClicked.Invoke();
    }
    
    public void Plant(PlantInfo info) {
        if (plant) {
            Destroy(plant);
        }
    
        var go = Instantiate(
            info.plantPrefab,
            transform
        );
        plant = go.GetComponent<Plant>();
        plant.Spawn(gridPos);
        
        info.seedsLeft--;
        GameEvents.Planted.Invoke();
    }
}
