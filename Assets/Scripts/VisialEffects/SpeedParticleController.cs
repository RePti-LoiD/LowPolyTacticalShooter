using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SpeedParticleController : MonoBehaviour
{
    [SerializeField] private float particleSpeedTreshold;
    [SerializeField] private Vector2 minMax;
    
    private ParticleSystem speedParticles;
    ParticleSystem.EmissionModule emmision;

    private void Awake()
    {
        speedParticles = GetComponent<ParticleSystem>();
        emmision = speedParticles.emission;
    }

    public void ForceDisableParticleSystem(float horizontalSpeed)
    {
        if (horizontalSpeed > particleSpeedTreshold)
        {
            speedParticles.Play();
            emmision.rateOverTime = new ParticleSystem.MinMaxCurve(minMax.x * horizontalSpeed, minMax.y * horizontalSpeed);
        }
        else
            speedParticles.Stop();
    }  
}