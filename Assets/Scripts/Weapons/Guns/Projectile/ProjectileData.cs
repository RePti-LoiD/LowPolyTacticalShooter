using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public float Damage;
    public float Speed;
    public float TargetYAngle;

    [Space]
    public AnimationCurve YCurve;
    public GunAPI GunAPI;
    public ProjectileThrower ProjectileThrower;
}