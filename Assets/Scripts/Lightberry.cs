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
        switch (_state) {
            case State.Growing:
                if (_timeToGrow > 0) {
                    _timeToGrow--;
                    break;
                }
                
                Grow();
                break;
            case State.Grown:
                ApplyWind(weather, neighbor);
                break;
        }
        
        base.DayTick(weather, neighbor);
    }
    
    void ApplyWind(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if ((int)(weather & Weather.Windy) == 0) return;
        if (immuneToWind) return;
        
        var dir = Tools.WindDirection(weather);
        var n = neighbor(GridPos, dir.x, dir.y);
        
        Harvest();
        
        if (!n) return;
        if (!n.plant) {
            n.Plant(info);
        } 
    }
    
    void Grow() {
        if (_state != State.Growing) return;
        
        _state = State.Grown;
        grownView.SetActive(true);
        grownView.transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBounce);
    }

    public override void Harvest() {
        if (_state != State.Grown) return;
        
        // TODO add to the total score
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
        
        base.Harvest();
    }
}