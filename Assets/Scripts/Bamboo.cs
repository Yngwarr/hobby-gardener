using System;
using DG.Tweening;
using UnityEngine;

public class Bamboo : Plant
{
    enum State {
        Growing,
        Grown
    }
    
    [SerializeField] GameObject youngView;
    [SerializeField] GameObject grownView;
    
    const int TTG = 2;
    
    State _state = State.Growing;
    int _timeToGrow = TTG;

    public override void Spawn(Vector2Int gridPos) {
        base.Spawn(gridPos);
        
        youngView.transform.DOScale(new Vector3(1, 0, 1), .5f).From()
            .SetEase(Ease.OutBounce);
    }

    public override void DayTick(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        switch (_state) {
            case State.Growing:
                if (_timeToGrow > 0) {
                    _timeToGrow--;
                    break;
                }
                
                Grow();
                break;
            case State.Grown:
                ApplyWindShield(weather, neighbor);
                break;
        }
        
        base.DayTick(weather, neighbor);
    }
    
    void Grow() {
        if (_state != State.Growing) return;
        
        _state = State.Grown;
        grownView.SetActive(true);
        grownView.transform.DOScale(new Vector3(1, 0, 1), .7f).From()
            .SetEase(Ease.OutQuad);
    }
    
    void ApplyWindShield(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if ((int)(weather & Weather.Windy) == 0) return;
        
        var dir = Tools.WindDirection(weather);
        var ns = new[] {
            neighbor(GridPos, dir.x, dir.y),
            neighbor(GridPos, dir.x * 2, dir.y * 2)
        };
        
        foreach (var n in ns) {
            if (!n) continue;
            if (!n.plant) continue;
            
            n.plant.immuneToWind = true;
        }
    }

    public override void Harvest() {
        if (_state != State.Grown) return;
        // TODO add score
        _state = State.Growing;
        _timeToGrow = TTG;
        grownView.SetActive(false);
        
        base.Harvest();
    }
}