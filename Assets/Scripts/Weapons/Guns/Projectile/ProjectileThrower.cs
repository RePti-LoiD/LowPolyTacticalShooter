using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;

    [SerializeField] private Transform muzzle;

    [Space]
    [SerializeField] private ProjectileData projectileData;

    public void SendProjectile()
    {
        var projectile = pool.Get();
        projectile.SetProjectileData(projectileData);
        
        projectile.transform.position = muzzle.transform.position;
        projectile.transform.rotation = muzzle.rotation;

        projectile.LaunchProjectile();
    }
}
