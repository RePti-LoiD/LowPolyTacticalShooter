using UnityEngine;

[CreateAssetMenu(fileName = "GunScopeData", menuName = "Scriptable Objects/GunParts/GunScopeData")]
public class GunScopeData : ScriptableObject
{
    [SerializeField] private string gunScopeName;
    [SerializeField] private float scopeFov;

    [SerializeField] private Vector3 aimPosition;

    public string GunScopeName { get => gunScopeName; }
    public float ScopeFov { get => scopeFov; set => scopeFov = value; }

    public Vector3 AimPosition { get => aimPosition; }
}