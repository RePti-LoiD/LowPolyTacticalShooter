using UnityEngine;

namespace WeaponBehaviour
{
    public class InstantTransform : MonoBehaviour
    {
        [Header("Position / Rotation")]
        [SerializeField] public Vector3 instantPosition;
        [SerializeField] public Vector3 instantRotation;
    }
}