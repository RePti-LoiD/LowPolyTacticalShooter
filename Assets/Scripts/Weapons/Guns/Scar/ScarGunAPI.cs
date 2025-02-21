using System.Collections;
using UnityEngine;

public class ScarGunAPI : BaseGunAPI
{
    [SerializeField] private LinearInterpolationAnim lerpAnim;
    [SerializeField] private RecoilRotationSender cameraRecoil;

    [SerializeField] private ProceduralPositioner proceduralPositioner;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        cameraRecoil.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void DisableGun()
    {
        cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
        gunDisabled?.Invoke();

        StartCoroutine(Wait(.15f));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        base.DisableGun();
    }
}