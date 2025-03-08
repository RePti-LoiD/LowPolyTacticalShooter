using System;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] public readonly GunAmmoData GunAmmoData;
    
    public Action<GunAmmo> GunAmmoDataChanged;

    private int currentAmmoCount;
    private int remainAmmoCount;

    public int CurrentAmmoCount
    {
        get => currentAmmoCount;
        private set
        {
            currentAmmoCount = value;

            GunAmmoDataChanged?.Invoke(this);
        }
    }

    public int RemainAmmoCount
    {
        get => remainAmmoCount;
        private set
        {
            remainAmmoCount = value;

            GunAmmoDataChanged?.Invoke(this);
        }
    }

    private void Start()
    {
        currentAmmoCount = GunAmmoData.MaxAmmoCount;
        remainAmmoCount = GunAmmoData.MaxRemainAmmoCount;
    }

    public bool TryTakeAmmo()
    {
        if (CurrentAmmoCount <= 0) return false;

        CurrentAmmoCount--;
        return true;
    }

    public void RefillAmmo()
    {
        if (currentAmmoCount == GunAmmoData.MaxAmmoCount) return;

        RemainAmmoCount -= Mathf.Abs(GunAmmoData.MaxAmmoCount - currentAmmoCount);
        CurrentAmmoCount = GunAmmoData.MaxAmmoCount;
    }
}

[CreateAssetMenu(fileName = "GunAmmoData", menuName = "Scriptable Objects/GunParts/GunAmmoData")]
public class GunAmmoData : ScriptableObject
{
    [SerializeField] private int maxAmmoCount;
    [SerializeField] private int maxRemainAmmoCount;
    [SerializeField] private int shotPerMinute;

    [Space]
    [SerializeField] private int bulletDamage;
    [SerializeField] private int bulletSpeed;

    public int MaxAmmoCount { get => maxAmmoCount; }
    public int MaxRemainAmmoCount { get => maxRemainAmmoCount; }
    public int ShotPerMinute { get => shotPerMinute; }
    public int BulletDamage { get => bulletDamage; }
    public int BulletSpeed { get => bulletSpeed; }
}