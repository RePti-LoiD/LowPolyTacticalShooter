using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GunSwitch gunSwitch;

    [SerializeField] private Transform targetGunTransform;
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private float raycastDistance;
    [SerializeField] private float raycastZOffset;

    [SerializeField] private int weaponLayerNumber;

    [Space]
    [SerializeField] private UnityEvent<GunAPI> gunSelected;
    [SerializeField] private UnityEvent gunDeselected;

    private GunAPI interactedGunAPI;

    private void Start()
    {
        if (raycastOrigin == null)
            raycastOrigin = Camera.main.transform;
    }

    public void OnInteraction()
    {
        if (interactedGunAPI != null)
        {
            gunDeselected?.Invoke();

            gunSwitch.AddGun(interactedGunAPI);
            gunSwitch.SelectGun(interactedGunAPI);

            if (interactedGunAPI.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            
            interactedGunAPI.transform.parent = targetGunTransform;
            interactedGunAPI.gameObject.transform.SetAllChildrenLayer(weaponLayerNumber);

            interactedGunAPI = null;
        }
    }

    private void Update()
    {
        var startPosition = new Vector3(raycastOrigin.localPosition.x, raycastOrigin.localPosition.y, raycastOrigin.localPosition.z + raycastZOffset);
        Debug.DrawRay(raycastOrigin.TransformPoint(startPosition), raycastOrigin.forward * raycastDistance, Color.red);

        if (Physics.Raycast(raycastOrigin.TransformPoint(startPosition), raycastOrigin.forward, out var hit, raycastDistance, targetLayer))
        {
            if (hit.transform.gameObject.TryGetComponent(out GunAPI gunApi))
            {
                if (interactedGunAPI == null)
                    gunSelected?.Invoke(gunApi);

                if (interactedGunAPI != null && interactedGunAPI != gunApi)
                {
                    gunDeselected?.Invoke();
                    gunSelected?.Invoke(gunApi);
                }


                interactedGunAPI = gunApi;
            }
        }
        else
        {
            if (interactedGunAPI != null)
                gunDeselected?.Invoke();

            interactedGunAPI = null;
        }
    }
}