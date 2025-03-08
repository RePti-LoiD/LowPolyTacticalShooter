using UnityEngine;

public class AKGunApi : BaseGunAPI
{
    [SerializeField] private ProjectileThrower projectileThrower;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        projectileThrower.SetPool(FindAnyObjectByType<ProjectileObjectPool>());
    }
}
