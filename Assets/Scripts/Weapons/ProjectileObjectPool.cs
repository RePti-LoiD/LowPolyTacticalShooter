using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab; 
    private ObjectPool<Projectile> pool;

    private void Start()
    {
        pool = new ObjectPool<Projectile>(OnCreate, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, 100);
    }

    private Projectile OnCreate()
    {
        var instance = Instantiate(projectilePrefab);
        instance.SetObjectPool(pool);

        return instance;
    }

    private void OnGetFromPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Projectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Projectile Get()
    {
        return pool.Get();
    }

    public void Release(Projectile pooledObject)
    {
        pool.Release(pooledObject);
    }
}
