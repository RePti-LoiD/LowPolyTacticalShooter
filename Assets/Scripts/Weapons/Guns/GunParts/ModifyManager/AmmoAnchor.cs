using UnityEngine;

public class AmmoAnchor : ModifierAnchor
{
    private GunAmmo currentGunAmmo;

    public override void EnableModifier(GameObject modifier)
    {
        base.EnableModifier(modifier);

        currentGunAmmo = modifier.GetComponent<GunAmmo>();
    }

    public void RefillAmmo()
    {
        currentGunAmmo.RefillAmmo();
    }
}