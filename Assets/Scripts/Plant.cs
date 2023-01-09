using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] protected PlantInfo info;
    protected Vector2Int GridPos;
    
    public virtual void Spawn(Vector2Int gridPos) {
        GridPos = gridPos;
    }
    
    public virtual void DayTick(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {}
    
    public virtual void Harvest() {}
}