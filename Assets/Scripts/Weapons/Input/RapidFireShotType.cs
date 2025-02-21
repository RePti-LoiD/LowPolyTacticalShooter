using UnityEngine;

public class RapidFireShotType : ShotType
{
    [SerializeField] private int shotPerMinute;

    private float shotDelay;
    private float lastShotTime = 0;

    private bool currentlyShot;

    private void Start()
    {
        shotDelay = 60 / (float)shotPerMinute;
    }

    private void Update()
    {
        if (!currentlyShot) return;
        if (Time.time - lastShotTime < shotDelay) return;

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