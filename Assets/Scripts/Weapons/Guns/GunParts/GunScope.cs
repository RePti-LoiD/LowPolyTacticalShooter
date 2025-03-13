using UnityEngine;

public class GunScope : MonoBehaviour
{
    [SerializeField] private GunScopeData currentGunScopeData;

    public GunScopeData GunScopeData { get => currentGunScopeData; }
}