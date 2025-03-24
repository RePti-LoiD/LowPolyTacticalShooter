using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileRotation projectileRotation;
    [SerializeField] public ProjectileCollisionDetection CollisionDetection;

    public UnityEvent ProjectileCollided;

    private ObjectPool<Projectile> pool;
    public ProjectileData CurrentProjectileData;

    public void SetObjectPool(ObjectPool<Projectile> pool) =>
        this.pool = pool;

    public void SetProjectileData(ProjectileData projectileData) =>
        CurrentProjectileData = projectileData;

    public void LaunchProjectile() =>
        StartCoroutine(Launch());

    private IEnumerator Launch()
    {
        var maxTime = CurrentProjectileData.YCurve.keys.Last().time;

        if (projectileRotation != null) 
            StartCoroutine(projectileRotation.RotateProjectile(CurrentProjectileData.TargetYAngle, maxTime));

        var currentTime = 0f;
        var lastEvaluate = 0f;

        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;

            transform.position += transform.forward * CurrentProjectileData.Speed * Time.deltaTime + new Vector3(0, CurrentProjectileData.YCurve.Evaluate(currentTime) - lastEvaluate);
            lastEvaluate = CurrentProjectileData.YCurve.Evaluate(currentTime);

            yield return null;
        }

        StopAllCoroutines();

        ReleaseProjectileToPool();
    }

    public void OnProjectileCollide(Projectile projectile, RaycastHit hitInfo)
    {
        CurrentProjectileData.ProjectileThrower.OnProjectileHit(this, hitInfo);
        ReleaseProjectileToPool();
    }

    public void ReleaseProjectileToPool() =>
        pool?.Release(this);
}