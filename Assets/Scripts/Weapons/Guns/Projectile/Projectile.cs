using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileRotation projectileRotation;

    public UnityEvent ProjectileCollided;

    private ObjectPool<Projectile> pool;
    public ProjectileData currentProjectileData;

    public void SetObjectPool(ObjectPool<Projectile> pool) =>
        this.pool = pool;

    public void SetProjectileData(ProjectileData projectileData) =>
        currentProjectileData = projectileData;

    public void LaunchProjectile() =>
        StartCoroutine(Launch());

    private IEnumerator Launch()
    {
        var maxTime = currentProjectileData.YCurve.keys.Last().time;

        if (projectileRotation != null) 
            StartCoroutine(projectileRotation.RotateProjectile(-10, 5));

        var currentTime = 0f;

        var lastEvaluate = 0f;
        var lastPosition = transform.position;

        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;

            transform.position += transform.forward * currentProjectileData.Speed * Time.deltaTime + new Vector3(0, currentProjectileData.YCurve.Evaluate(currentTime) - lastEvaluate);
            lastEvaluate = currentProjectileData.YCurve.Evaluate(currentTime);

            yield return null;
        }

        StopAllCoroutines();
        pool?.Release(this);
    }
}