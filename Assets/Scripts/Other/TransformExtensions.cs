using System;
using UnityEngine;

public static class TransformExtensions
{
    public static Vector2 Clamp(this Vector2 value, Vector2 clampVector) =>
        new Vector2(
            Math.Clamp(value.x, -clampVector.x, clampVector.x),
            Math.Clamp(value.y, -clampVector.y, clampVector.y)
        );

    public static Vector3 Clamp(this Vector3 a, Vector3 clampVector) =>
        new Vector3(
            Math.Clamp(a.x, -clampVector.x, clampVector.x),
            Math.Clamp(a.y, -clampVector.y, clampVector.y),
            Math.Clamp(a.z, -clampVector.z, clampVector.z)
        );

    public static void LerpLocal(this Transform a, Transform b, float amount)
    {
        a.localPosition = Vector3.Lerp(a.localPosition, b.localPosition, amount);
        a.localRotation = Quaternion.Lerp(a.localRotation, b.localRotation, amount);
    }

    public static void LerpUnclampedLocal(this Transform a, Transform b, float amount)
    {
        a.localPosition = Vector3.LerpUnclamped(a.localPosition, b.localPosition, amount);
        a.localRotation = Quaternion.LerpUnclamped(a.localRotation, b.localRotation, amount);
    }

    public static void SlerpUnclampedLocal(this Transform a, Transform b, float amount)
    {
        a.localPosition = Vector3.SlerpUnclamped(a.localPosition, b.localPosition, amount);
        a.localRotation = Quaternion.SlerpUnclamped(a.localRotation, b.localRotation, amount);
    }
}