using UnityEngine;

public class RailgunAPI : BaseGunAPI
{
    [SerializeField] private RecoilRotationSender RecoilRotationSender;

    [SerializeField] private LinearInterpolationAnim lerpAnim;

    public void SetJumpVelocity(float jumpVelocity)
    {
        LastData.Movement.SetJumpVelocity(jumpVelocity);
    }

    public void AddMovementAdditionalVelocity(Vector3 additionalVelocity)
    {
        LastData.Movement.AddLinearVelocity(additionalVelocity);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        lerpAnim.AnimateGunEnabling();

        RecoilRotationSender.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void DisableGun()
    {
        RecoilRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);

        lerpAnim.AnimateGunDisabling(() => base.DisableGun());
    }
}