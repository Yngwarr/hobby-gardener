using System;
using DG.Tweening;
using UnityEngine;

public class Lightberry : Plant
{
    enum State {
        Growing,
        Grown,
        Dead
    }
    
    [SerializeField] GameObject youngView;
    [SerializeField] GameObject grownView;
    
    const int TTG = 3;
    
    State _state = State.Growing;
    int _timeToGrow = TTG;
    int _fruitNum = 3;
    
    public override void Spawn(Vector2Int gridPos) {
        base.Spawn(gridPos);
            
        youngView.transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBounce);
    }
    
    public override void DayTick(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        base.DayTick(weather, neighbor);

        switch (_state) {
            case State.Growing:
                if (_timeToGrow > 0) {
                    _timeToGrow--;
                    return;
                }
                
                Grow();
                break;
            case State.Grown:
                ApplyWind(weather, neighbor);
                break;
        }
    }
    
    void ApplyWind(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if ((int)(weather & Weather.Windy) == 0) return;
        
        var dir = Tools.WindDirection(weather);
        var n = neighbor(GridPos, dir.x, dir.y);
        
        if (n == null) return;
        if (n.plant != null) return;
        
        n.Plant(info);
        Harvest();
    }
    
    void Grow() {
        if (_state != State.Growing) return;
        
        _state = State.Grown;
        grownView.SetActive(true);
        grownView.transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBounce);
    }

    public override void Harvest() {
        if (_state != State.Grown) return;
        
        // TODO add to a total score
        _fruitNum--;
        
        if (_fruitNum == 0) {
            _state = State.Dead;
            // TODO set correct visuals
            grownView.SetActive(false);
            youngView.transform.DOScaleX(1, .3f);
            return;
        }
        
        _state = State.Growing;
        _timeToGrow = TTG;
        // TODO add some particle effect
        grownView.SetActive(false);
    }
}