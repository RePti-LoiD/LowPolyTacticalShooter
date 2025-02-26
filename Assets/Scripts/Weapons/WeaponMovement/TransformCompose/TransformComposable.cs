using UnityEngine;

public abstract class TransformComposable : MonoBehaviour
{
    public abstract Vector3 GetPosition(Vector3 prevPosition);
    public abstract Quaternion GetRotation(Quaternion prevRotation);
}