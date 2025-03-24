using UnityEngine;

public class ModifierAnchor : MonoBehaviour
{
    protected GameObject currentModifier;

    public virtual void SpawnModifier(GameObject modifier)
    {
        if (currentModifier != null)
            DestroyModifier();

        currentModifier = Instantiate(modifier, transform, false);

        currentModifier.transform.localPosition = Vector3.zero;
        currentModifier.transform.localRotation = Quaternion.identity;
        currentModifier.transform.SetAllChildrenLayer(gameObject.layer);
    }

    public virtual void EnableModifier(GameObject modifier)
    {
        currentModifier.SetActive(true);
    }

    public virtual void DestroyModifier()
    {
        Destroy(currentModifier);
    }
}
