using UnityEngine;
using UnityEngine.Events;

public class GunInputHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent<Projectile, GunAPI, GameObject> projectileHit;

    private GunAPI currentGun;

    public void SetGunForInput(GunAPI gun)
    {
        if (currentGun != null)
            currentGun.ProjectileHit -= InvokeProjectileHit;

        currentGun = gun;

        currentGun.ProjectileHit += InvokeProjectileHit;
    }

    private void InvokeProjectileHit(Projectile projectile, GunAPI gunAPI, GameObject hitObject) =>
        projectileHit?.Invoke(projectile, gunAPI, hitObject);

    public void OnShotStart() =>
        currentGun?.ShotStart();

    public void OnShotStop() =>
        currentGun?.ShotStop();

    public void OnAdditionalAction() =>
        currentGun?.AdditionalAction();

    public void OnAdditionalActionStop() =>
        currentGun?.AdditionalActionStop();

    public void OnInspect() =>
        currentGun?.Inspect();

    public void OnReload() =>
        currentGun?.Reload();

    public void Modify() =>
        currentGun?.Modify();

    public void ModifyCanceled() =>
        currentGun?.ModifyCanceled();
}