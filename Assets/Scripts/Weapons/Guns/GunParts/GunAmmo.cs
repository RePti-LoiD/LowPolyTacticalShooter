using System;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] private GunAmmoData gunAmmoData;
    public GunAmmoData GunAmmoData { get => gunAmmoData; }
    
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

    private void OnEnable()
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