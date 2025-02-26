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

    private Vector3 lastPosition;
    private Vector3 currentPosition;

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


        currentPosition = transform.position;
        lastPosition = currentPosition;

        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;

            transform.position += transform.forward * CurrentProjectileData.Speed * Time.deltaTime + new Vector3(0, CurrentProjectileData.YCurve.Evaluate(currentTime) - lastEvaluate);
            lastEvaluate = CurrentProjectileData.YCurve.Evaluate(currentTime);
            
            CheckCollision();

            yield return null;
        }

        StopAllCoroutines();

        ReleaseProjectileToPool();
    }

    private void CheckCollision()
    {
        lastPosition = currentPosition;
        currentPosition = transform.position;

        var direction = currentPosition - lastPosition;

        Debug.DrawRay(lastPosition, direction, Color.red);

        if (Physics.Raycast(lastPosition, direction, out RaycastHit hit, direction.magnitude))
        {
            print(hit.collider.gameObject);

            CurrentProjectileData.ProjectileThrower.OnProjectileHit(this, hit);
            ReleaseProjectileToPool();
        }
    }

    public void ReleaseProjectileToPool() =>
        pool?.Release(this);
}