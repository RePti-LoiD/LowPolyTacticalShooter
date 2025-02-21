using UnityEngine;

public class RevolverAPI : BaseGunAPI
{
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private RecoilRotationSender cameraRecoil;

    [SerializeField] private LinearInterpolationAnim lerpAnim;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        lerpAnim.AnimateGunEnabling();

        soundPlayer.Source = data.AudioSource;
        cameraRecoil.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void DisableGun()
    {
        soundPlayer.Source = null;
        cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);

        lerpAnim.AnimateGunDisabling(() => base.DisableGun());
    }
}