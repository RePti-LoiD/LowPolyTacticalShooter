using UnityEngine;
using UnityEngine.Events;

public class RuntimeGunData : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private UnityEvent<RuntimeGunData> GunDataChanged;

    private int currentAmmoCount;
    private int remainAmmoCount;

    public GunData GunData { get => gunData; }
    public int CurrentAmmoCount 
    { 
        get => currentAmmoCount; 
        private set 
        {
            currentAmmoCount = value;
            
            GunDataChanged?.Invoke(this);
        }
    }
    
    public int RemainAmmoCount 
    { 
        get => remainAmmoCount; 
        private set
        {
            remainAmmoCount = value; 
            
            GunDataChanged?.Invoke(this);
        }
    }

    private void Start()
    {
        currentAmmoCount = gunData.MaxAmmoCount;
        remainAmmoCount = gunData.MaxRemainAmmo;
    }

    public bool TryTakeAmmo()
    {
        if (CurrentAmmoCount <= 0) return false;

        CurrentAmmoCount--;
        return true;
    }

    public void RefillAmmo()
    {
        if (currentAmmoCount == gunData.MaxAmmoCount) return;

        RemainAmmoCount -= Mathf.Abs(gunData.MaxAmmoCount - currentAmmoCount);
        CurrentAmmoCount = gunData.MaxAmmoCount;
    }
}