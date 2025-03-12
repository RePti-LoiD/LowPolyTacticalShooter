using UnityEngine;

public class ScarGunAPI : BaseGunAPI
{
    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);
    }

    public override void DisableGun()
    {
        DisableComponents();

        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;
    }
}