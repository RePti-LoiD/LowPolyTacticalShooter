using UnityEngine;

public class RapidFireShotType : ShotType
{
    private float shotDelay;
    private float lastShotTime = 0;

    private bool currentlyShot;

    private void Start()
    {
        shotDelay = Minute / (float)RuntimeGunData.GunAmmo.GunAmmoData.ShotPerMinute;
    }

    private void Update()
    {
        if (!currentlyShot) return;
        if (Time.time - lastShotTime < shotDelay) return;
        if (!RuntimeGunData.GunAmmo.TryTakeAmmo()) return;

        OnShot?.Invoke();

        lastShotTime = Time.time;
    }

    public override void OnShotStart()
    {
        base.OnShotStart();
        currentlyShot = true;
    }

    public override void OnShotStop()
    {
        base.OnShotStop();
        currentlyShot = false;
    }
}