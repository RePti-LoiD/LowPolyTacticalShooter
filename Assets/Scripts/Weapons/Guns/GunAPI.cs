using System;
using UnityEngine;

public abstract class GunAPI : MonoBehaviour
{
    [SerializeField] public GunIkAPI ArmsIkAPI;
    [SerializeField] public RuntimeGunData GunData;

    public Action<RuntimeGunData> GunDataChanged;
    public Action<GunAPI> Disabled;

    public Action<Projectile, GunAPI, GameObject> ProjectileHit;

    public abstract void ShotStart();
    public abstract void ShotStop();
    public abstract void Reload();
    public abstract void AdditionalAction();
    public abstract void AdditionalActionStop();
    public abstract void Inspect();

    public abstract void Modify();
    public abstract void ModifyCanceled();

    public abstract void EnableGun(ExternalDataForGun data);

    public virtual void DisableGun() =>
        Disabled?.Invoke(this);

    public virtual void ForceStop() { }

    public void OnGunDataChanged(RuntimeGunData gunData)
    {
        GunDataChanged?.Invoke(gunData);
    }

    public void InvokeProjectileHit(Projectile projectile, RaycastHit hit)
    {
        ProjectileHit?.Invoke(projectile, this, hit.collider.gameObject);
    }
}