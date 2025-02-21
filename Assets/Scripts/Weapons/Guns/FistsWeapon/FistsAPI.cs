using UnityEngine;

public class FistsAPI : BaseGunAPI
{
    [SerializeField] public RecoilRotationSender leftHitRotationSender;
    [SerializeField] public RecoilRotationSender rightHitRotationSender;

    [SerializeField] private LinearInterpolationAnim lerpAnim;

    public override void DisableGun()
    {
        leftHitRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
        rightHitRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);

        lerpAnim.AnimateGunDisabling(() => base.DisableGun());
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        lerpAnim.AnimateGunEnabling();

        leftHitRotationSender.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
        rightHitRotationSender.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
    }
}