using UnityEngine;

public class ProjectileImpactParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystemsPrefabs;
    [SerializeField] private float particlesLifetime;

    [SerializeField] private float debugRayLength;

    public void OnProjectileHit(Projectile projectile, RaycastHit hitInfo)
    {
        print(hitInfo.collider.name);

        var particles = Instantiate (
            particleSystemsPrefabs[Random.Range(0, particleSystemsPrefabs.Length)], 
            hitInfo.point, 
            Quaternion.identity, 
            null
        );

        particles.Play();

        particles.transform.rotation = Quaternion.LookRotation(hitInfo.normal);

        Destroy(particles.gameObject, particlesLifetime);
        Debug.DrawRay(hitInfo.point, hitInfo.normal * debugRayLength, Color.red, 5);
    }
}
