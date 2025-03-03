using UnityEngine;
using UnityEngine.Events;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] private Transform muzzle;

    [Space]
    [SerializeField] private UnityEvent<Projectile, RaycastHit> Collided;

    [Space]
    [SerializeField] private ProjectileData projectileData;

    private ObjectPool pool;

    public void SetPool(ObjectPool pool) =>
        this.pool = pool;

    public void SendProjectile()
    {
        var projectile = pool.Get();
        projectile.SetProjectileData(projectileData);
        
        projectile.transform.position = muzzle.transform.position;
        projectile.transform.rotation = muzzle.rotation;

        projectile.LaunchProjectile();
    }

    public void OnProjectileHit(Projectile projectile, RaycastHit hitInfo)
    {
        Collided?.Invoke(projectile, hitInfo);
    }
}
