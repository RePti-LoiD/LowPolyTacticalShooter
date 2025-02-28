using System.Collections.Generic;
using UnityEngine;
using WeaponBehaviour;

[RequireComponent(typeof(InstantTransform))]
public class TransformCompositor : RootPositioning
{
    [SerializeField] private List<TransformComposableData> composablesData;
    [SerializeField] private bool dropTransformAfterCompose;

    private Vector3 additionalPosition;
    private Quaternion additionalRotation;

    public void Update()
    {
        CompositeTransform();

        ApplyTransform();
        
        if (dropTransformAfterCompose)
            DropTransform();
    }

    public void CompositeTransform()
    {
        foreach (var composableData in composablesData)
        {
            if (composableData.ComposePosition)
                additionalPosition += Vector3.Lerp(Vector3.zero, composableData.Composable.GetPosition(additionalPosition), composableData.Weight);

            if (composableData.ComposeRotation)
                additionalRotation *= Quaternion.Slerp(Quaternion.identity, composableData.Composable.GetRotation(additionalRotation), composableData.Weight);
        }
    }

    public void ApplyTransform()
    {
        transform.localPosition = instantTransform.instantPosition + additionalPosition;
        transform.localRotation = Quaternion.Euler(instantTransform.instantRotation) * additionalRotation;
    }

    public void DropTransform()
    {
        additionalPosition = Vector3.zero;
        additionalRotation = Quaternion.identity;
    }
}