using UnityEngine;
using UnityEngine.Events;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] private RuntimeGunData runtimeGunData;
    [SerializeField] private Transform muzzle;

    [Space]
    [SerializeField] private UnityEvent<Projectile, RaycastHit> Collided;

    [Space]
    [SerializeField] private ProjectileData projectileData;

    private ProjectileObjectPool pool;

    public void SetPool(ProjectileObjectPool pool) =>
        this.pool = pool;

    public void SendProjectile()
    {
        var projectile = pool.Get();

        projectile.SetProjectileData(new ProjectileData
        {
            Damage = runtimeGunData.GunAmmo.GunAmmoData.BulletDamage,
            Speed = runtimeGunData.GunAmmo.GunAmmoData.BulletSpeed,
            GunAPI = projectileData.GunAPI,
            ProjectileThrower = this,
            TargetYAngle = projectileData.TargetYAngle,
            YCurve = projectileData.YCurve
        });
        
        projectile.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.rotation);
        projectile.LaunchProjectile();
    }

    public void OnProjectileHit(Projectile projectile, RaycastHit hitInfo)
    {
        Collided?.Invoke(projectile, hitInfo);
    }
}
