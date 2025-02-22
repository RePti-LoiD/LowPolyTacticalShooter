using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public UnityEvent ProjectileCollided;

    private ObjectPool<Projectile> pool;
    private ProjectileData currentProjectileData;

    private void Update() =>
        transform.position += transform.forward * currentProjectileData.Speed * Time.deltaTime;

    public void SetObjectPool(ObjectPool<Projectile> pool) =>
        this.pool = pool;

    public void SetProjectileData(ProjectileData projectileData) =>
        currentProjectileData = projectileData;

    public void LaunchProjectile() =>
        StartCoroutine(Destroy(currentProjectileData.Lifetime));

    private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);

        pool.Release(this);
    }
}