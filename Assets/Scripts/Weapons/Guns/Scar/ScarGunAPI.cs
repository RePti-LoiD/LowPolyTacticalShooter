using System.Collections;
using UnityEngine;

public class ScarGunAPI : BaseGunAPI
{
    [SerializeField] private RecoilRotationSender cameraRecoil;

    [SerializeField] private ProjectileThrower projectileThrower;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);
        projectileThrower.SetPool(data.ProjectilePool);

        cameraRecoil.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void DisableGun()
    {
        cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
        
        gunDisabled?.Invoke();
        StopAllCoroutines();

        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;
    }

    public void OnAnimationEnded(AnimationUnit animationUnit)
    {
        print("jopa");

        if (animationUnit.AnimationName == "HoldDown")
            Disabled?.Invoke(this);
    }
}