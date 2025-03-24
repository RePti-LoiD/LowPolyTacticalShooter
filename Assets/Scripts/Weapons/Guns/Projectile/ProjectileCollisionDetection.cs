using UnityEngine;
using UnityEngine.Events;

public class ProjectileCollisionDetection : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] public UnityEvent<Projectile, RaycastHit> Collided;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private void OnEnable()
    {
        currentPosition = transform.position;
        lastPosition = currentPosition;
    }

    public void Update()
    {
        lastPosition = currentPosition;
        currentPosition = transform.position;

        var direction = currentPosition - lastPosition;

        Debug.DrawRay(lastPosition, direction, Color.red);

        if (Physics.Raycast(lastPosition, direction, out RaycastHit hit, direction.magnitude))
        {
            Collided?.Invoke(projectile, hit);            
        }
    }
}