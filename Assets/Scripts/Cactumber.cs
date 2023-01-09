using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cactumber : Plant
{
    enum State {
        Growing,
        Grown
    }
    
    [SerializeField] GameObject youngView;
    [SerializeField] GameObject grownView;
    
    const int TTG = 3;
    
    State _state = State.Growing;
    int _timeToGrow = TTG;
    bool gaveBirth;

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
                ApplyRain(weather, neighbor);
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
    
    void ApplyRain(Weather weather, Func<Vector2Int, int, int, Soil> neighbor) {
        if (weather != Weather.Rain) return;
        if (gaveBirth) return;
        
        var ns = new[] {
            neighbor(GridPos, 0, -1),
            neighbor(GridPos, -1, 0),
            neighbor(GridPos, 0, 1),
            neighbor(GridPos, 1, 0)
        };

        var empty = new List<Soil>();
        foreach (var n in ns) {
            if (!n) continue;
            if (n.plant) continue;
            empty.Add(n);
        }
        
        if (empty.Count == 0) return;
        
        empty[Random.Range(0, empty.Count)].Plant(info);
        gaveBirth = true;
    }

    public override void Harvest() {
        base.Harvest();
        
        if (_state != State.Grown) return;
        // TODO add score
        Destroy(gameObject);
    }
}