using UnityEngine;

public class SingleShotType : ShotType
{
    [SerializeField] private float fireRatePerMinute;

    private const int MINUTE = 60;

    private float durationBetweenShot;
    private float lastShotTime = 0;

    private void Start()
    {
        durationBetweenShot = MINUTE / fireRatePerMinute;
    }

    private void OnEnable()
    {
        lastShotTime = 0;
    }

    public override void OnShotStart()
    {
        if (Time.time - lastShotTime < durationBetweenShot) return;
        lastShotTime = Time.time;

        OnShot?.Invoke();
    }
}