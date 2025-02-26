using System;
using UnityEngine;

[Serializable]
public class TransformComposableData
{
    [SerializeReference]
    public TransformComposable Composable;
    [Range(0, 1)] public float Weight;

    public bool ComposePosition;
    public bool ComposeRotation;
}