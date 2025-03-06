using UnityEngine;

public class ScarGunAPI : BaseGunAPI
{

    [SerializeField] private ProjectileThrower projectileThrower;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);
        projectileThrower.SetPool(data.ProjectilePool);
    }

    public override void DisableGun()
    {
        DisableComponents();

        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;
    }
}