using UnityEngine;

public class GunScope : MonoBehaviour
{
    [SerializeField] private GunScopeData currentGunScopeData;
    public Transform TargetCameraPosition;

    public GunScopeData GunScopeData { get => currentGunScopeData; }
}