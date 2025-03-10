using UnityEngine;

public class GunScope : MonoBehaviour
{
    [SerializeField] private GunScopeData gunScopeData;
    [SerializeField] private GunScope additionalScope;

    public GunScope NextScope() =>
        additionalScope;
}

[CreateAssetMenu(fileName = "GunScopeData", menuName = "Scriptable Objects/GunParts/GunScopeData")]
public class GunScopeData : ScriptableObject
{
    [SerializeField] private string gunScopeName;
    [SerializeField] private Transform aimPosition;
    [SerializeField] private Vector3 muzzlePositionOffset;

    public Transform AimPosition { get => aimPosition; }
    public Vector3 MuzzlePositionOffset { get => muzzlePositionOffset; }
    public string GunScopeName { get => gunScopeName; }
}