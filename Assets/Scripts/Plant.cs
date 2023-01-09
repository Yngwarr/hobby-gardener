using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] protected PlantInfo info;
    protected Vector2Int GridPos;
    
    [HideInInspector] public bool immuneToWind;
    
    public virtual void Spawn(Vector2Int gridPos) {
        GridPos = gridPos;
        info.seedsLeft--;
        GameEvents.PlantSpawned.Invoke();
    }
    
    public virtual void DayTick(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        immuneToWind = false;
    }
    
    public virtual void Harvest() {}
}