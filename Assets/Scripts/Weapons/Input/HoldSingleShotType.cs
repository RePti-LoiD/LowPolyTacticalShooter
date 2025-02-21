using UnityEngine;
using UnityEngine.Events;

public class HoldSingleShotType : ShotType
{
    [SerializeField] private float minHoldTime;
    [SerializeField] public UnityEvent<float> OnShotFloat;
    [SerializeField] public UnityEvent<float> ShotHold;

    private float lastShotTime;
    private bool currentlyShot;

    private void Update()
    {
        if (currentlyShot)
            ShotHold?.Invoke(Time.time - lastShotTime);
    }

    public override void OnShotStart()
    {
        currentlyShot = true;

        lastShotTime = Time.time;
    }

    public override void OnShotStop()
    {
        if (!currentlyShot) return;

        currentlyShot = false;

        var currentHoldTime = Time.time - lastShotTime;
        if (currentHoldTime < minHoldTime) return;

        OnShotFloat?.Invoke(currentHoldTime);  
    }
}