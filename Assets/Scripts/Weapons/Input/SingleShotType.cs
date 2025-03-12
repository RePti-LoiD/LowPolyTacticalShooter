using UnityEngine;

public class SingleShotType : ShotType
{
    private float durationBetweenShot;
    private float lastShotTime = 0;

    private void Start()
    {
        durationBetweenShot = Minute / RuntimeGunData.GunAmmo.GunAmmoData.ShotPerMinute;
    }

    private void OnEnable()
    {
        lastShotTime = 0;
    }

    public override void OnShotStart()
    {
        if (Time.time - lastShotTime < durationBetweenShot) return;
        if (!RuntimeGunData.GunAmmo.TryTakeAmmo()) return;

        OnShot?.Invoke();

        lastShotTime = Time.time;
    }
}