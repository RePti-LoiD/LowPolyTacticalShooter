using UnityEngine;

public class CzGunApi : BaseGunAPI
{
    [SerializeField] private LinearInterpolationAnim lerpAnim;
    [SerializeField] private RecoilRotationSender cameraRecoil;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        cameraRecoil.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);

        lerpAnim.AnimateGunEnabling();
    }

    public override void DisableGun()
    {
        lerpAnim.AnimateGunDisabling(() => base.DisableGun());
        cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void AdditionalActionStop()
    {
        base.AdditionalActionStop();
    }
}