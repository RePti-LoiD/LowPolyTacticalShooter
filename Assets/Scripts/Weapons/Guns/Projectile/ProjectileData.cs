using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public float Damage;
    public float Speed;
    public float Lifetime;
    public AnimationCurve YCurve;
    public GunAPI GunAPI;
}