using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public virtual void Spawn() {}
    public virtual void Tick(Func<int, int, Soil> neighbor) {}
    public virtual void Harvest() {}
}