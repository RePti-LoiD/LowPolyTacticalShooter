using UnityEngine;

[CreateAssetMenu(fileName = "GunGripData", menuName = "Scriptable Objects/GunParts/GunGripData")]
class GunGripData : ScriptableObject
{
    [SerializeField] private string gripName;

    [SerializeField] private Vector3 gripPosition;
    [SerializeField] private Vector3 gripRotation;
}