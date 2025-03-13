using UnityEngine;

[CreateAssetMenu(fileName = "GunMuzzleData", menuName = "Scriptable Objects/GunParts/GunMuzzleData")]
public class GunMuzzleData : ScriptableObject
{
    [SerializeField] private string muzzleName;

    [SerializeField] private AudioClip shotSound;
    [SerializeField] private ParticleSystem shotParticle;
}