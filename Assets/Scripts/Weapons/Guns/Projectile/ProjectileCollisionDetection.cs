using System;
using UnityEngine;

public class ProjectileCollisionDetection : MonoBehaviour
{
    [SerializeField] private Projectile projectile;

    public event Action<Projectile, RaycastHit> Collided;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private void OnEnable()
    {
        currentPosition = transform.position;
        lastPosition = currentPosition;

        print((transform.position, currentPosition, lastPosition));
    }

    public void Update()
    {
        lastPosition = currentPosition;
        currentPosition = transform.position;

        var direction = currentPosition - lastPosition;

        Debug.DrawRay(lastPosition, direction, Color.red);

        if (Physics.Raycast(lastPosition, direction, out RaycastHit hit, direction.magnitude))
        {
            print(hit.collider.gameObject);
            
            Collided?.Invoke(projectile, hit);
            projectile.CurrentProjectileData.ProjectileThrower.OnProjectileHit(projectile, hit);

            projectile.ReleaseProjectileToPool();
            
        }
    }
}