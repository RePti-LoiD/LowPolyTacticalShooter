using UnityEngine;

namespace WeaponBehaviour
{
    [RequireComponent(typeof(InstantTransform))]
    public class RootPositioning : MonoBehaviour
    {
        [Header("Root Position")]
        [SerializeField] protected InstantTransform instantTransform;

        protected Vector3 rootPosition;
        protected Quaternion rootRotation;

        protected void Awake()
        {
            instantTransform = instantTransform != null ? instantTransform : GetComponent<InstantTransform>();
            rootPosition = instantTransform.instantPosition;
            rootRotation = Quaternion.Euler(instantTransform.instantRotation);
        }
    }
}
