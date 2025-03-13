using UnityEngine;

public class AmmoAnchor : ModifierAnchor
{
    private GunAmmo currentGunAmmo;

    public override void SpawnModifier(GameObject modifier)
    {
        base.SpawnModifier(modifier);

        currentGunAmmo = modifier.GetComponent<GunAmmo>();
    }

    public void RefillAmmo()
    {
        currentGunAmmo.RefillAmmo();
    }
}