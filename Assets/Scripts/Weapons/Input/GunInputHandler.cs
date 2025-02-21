using UnityEngine;

public class GunInputHandler : MonoBehaviour
{
    private GunAPI currentGun;

    public void SetGunForInput(GunAPI gun)
    {
        currentGun = gun;
    }

    public void OnShotStart() =>
        currentGun?.ShotStart();

    public void OnShotStop() =>
        currentGun?.ShotStop();

    public void OnAdditionalAction() =>
        currentGun?.AdditionalAction();

    public void OnAdditionalActionStop() =>
        currentGun?.AdditionalActionStop();

    public void OnInspect() =>
        currentGun?.Inspect();

    public void OnReload() =>
        currentGun?.Reload();
}