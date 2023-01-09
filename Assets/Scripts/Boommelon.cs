using System;
using DG.Tweening;
using UnityEngine;

public class Boommelon : Plant
{
    enum State {
        Growing,
        Grown
    }
    
    [SerializeField] GameObject youngView;
    [SerializeField] GameObject grownView;
    
    const int TTG = 4;
    
    State _state = State.Growing;
    int _timeToGrow = TTG;

    public override void Spawn(Vector2Int gridPos) {
        base.Spawn(gridPos);
        
        youngView.transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBounce);
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
                ApplySun(weather, neighbor);
                break;
        }
        
        base.DayTick(weather, neighbor);
    }
    
    void Grow() {
        if (_state != State.Growing) return;
        
        _state = State.Grown;
        youngView.SetActive(false);
        grownView.SetActive(true);
        grownView.transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBounce);
    }
    
    void ApplySun(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if (weather != Weather.Sunny) return;
        
        var ns = new[] {
            neighbor(GridPos, 0, -1),
            neighbor(GridPos, -1, 0),
            neighbor(GridPos, 0, 1),
            neighbor(GridPos, 1, 0)
        };

        foreach (var n in ns) {
            if (!n) continue;
            if (n.plant is Cactumber) continue;
            n.Plant(info);
        }
        
        Destroy(gameObject);
    }

    public override void Harvest() {
        if (_state != State.Grown) return;
        // TODO add score
        Destroy(gameObject);
        base.Harvest();
    }
}