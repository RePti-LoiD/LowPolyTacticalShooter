using UnityEngine;

public class MuzzleAnchor : ModifierAnchor
{
    [SerializeField] private ShotType shotType;
    private GunMuzzle muzzle;

    public override void SpawnModifier(GameObject modifier)
    {
        base.SpawnModifier(modifier);

        muzzle = currentModifier.GetComponent<GunMuzzle>();
        shotType.OnShot.AddListener(muzzle.OnShot);
    }

    public override void DestroyModifier()
    {
        shotType.OnShot.RemoveListener(muzzle.OnShot);
        base.DestroyModifier();
    }
}