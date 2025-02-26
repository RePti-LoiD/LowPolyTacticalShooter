using UnityEngine;

public abstract class TransformComposable : MonoBehaviour
{
    public abstract Vector3 GetPosition();
    public abstract Quaternion GetRotation();
}