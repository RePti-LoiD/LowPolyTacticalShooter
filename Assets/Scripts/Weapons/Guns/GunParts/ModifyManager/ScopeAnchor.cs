using UnityEngine;

class ScopeAnchor : ModifierAnchor
{
    [SerializeField] private ProceduralPositioner proceduralPositioner;
    [SerializeField] private Vector3 aimGlobalOffset = -new Vector3(0.1f, -0.16f, 0.25f);
    [SerializeField] private string aimPositionName = "Aim";

    private GunScope currentGunScope;

    public override void SpawnModifier(GameObject modifier)
    {
        base.SpawnModifier(modifier);

        currentGunScope = modifier.GetComponent<GunScope>();
        proceduralPositioner.GetAnimation(aimPositionName).TargetTransform.localPosition = CalcRelativePosition(proceduralPositioner.transform);
    }

    public Vector3 CalcRelativePosition(Transform parentTransform) =>
        aimGlobalOffset - parentTransform.worldToLocalMatrix.MultiplyPoint(transform.position) - currentGunScope.TargetCameraPosition.position;

}