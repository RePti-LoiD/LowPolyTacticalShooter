using UnityEngine;

[CreateAssetMenu(fileName = "RecoilRotationData", menuName = "Scriptable Objects/RecoilRotationData")]
public class RecoilRotationData : ScriptableObject
{
    public Vector3 rotation;
    public Vector3 targetRotation;

    [Space]
    public float returnTime;
    public float snappines;
}