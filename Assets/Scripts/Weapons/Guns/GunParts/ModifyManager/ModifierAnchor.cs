using UnityEngine;

public class ModifierAnchor : MonoBehaviour
{
    private GameObject currentModifier;

    public virtual void SpawnModifier(GameObject modifier)
    {
        if (currentModifier != null)
            DestroyModifier();

        currentModifier = Instantiate(modifier, transform, false);
        currentModifier.transform.localPosition = Vector3.zero;
        currentModifier.transform.localRotation = Quaternion.identity;
    }

    public virtual void DestroyModifier()
    {
        Destroy(currentModifier);
    }
}
