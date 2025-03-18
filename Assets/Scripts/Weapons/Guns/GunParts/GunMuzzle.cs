using UnityEngine;

public class GunMuzzle : MonoBehaviour
{
    [SerializeField] public GunMuzzleData muzzleData;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public ParticleSystem shotParticle;

    public void OnShot()
    {
        print($"gay club: {name}");
        shotParticle.Play();

        audioSource.clip = muzzleData.ShotSound[Random.Range(0, muzzleData.ShotSound.Length)];
        audioSource.Play();
    }
}
