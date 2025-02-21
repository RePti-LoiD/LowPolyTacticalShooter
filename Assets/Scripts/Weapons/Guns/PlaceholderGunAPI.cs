using UnityEngine;

public class PlaceholderGunAPI : GunAPI
{
    public override void AdditionalAction()
    {

    }

    public override void AdditionalActionStop()
    {

    }

    public override void DisableGun()
    {
        gameObject.SetActive(false);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        gameObject.SetActive(true);
    }

    public override void Inspect()
    {

    }

    public override void Reload()
    {

    }

    public override void ShotStart()
    {

    }

    public override void ShotStop()
    {

    }
}