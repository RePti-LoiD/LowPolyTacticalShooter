using System;
using UnityEngine;

[Serializable]
public class AnimationUnit
{
    [SerializeField] public string AnimationName;
    [SerializeField] public int AnimationIndex;

    [SerializeField] public Transform TargetTransform;
    [SerializeField] public AnimationCurve TimeCurve;
}