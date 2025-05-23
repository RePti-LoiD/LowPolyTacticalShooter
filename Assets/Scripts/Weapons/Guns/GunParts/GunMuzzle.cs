using UnityEngine;

public class GunMuzzle : MonoBehaviour
{
    [SerializeField] public GunMuzzleData muzzleData;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public ParticleSystem shotParticle;

    public void OnShot()
    {
        shotParticle.Play();
        audioSource.PlayOneShot(muzzleData.ShotSound[Random.Range(0, muzzleData.ShotSound.Length)]);
    }
}
