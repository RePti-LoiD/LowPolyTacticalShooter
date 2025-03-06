using UnityEngine;

public class WeaponSelectUi : MonoBehaviour
{
    [SerializeField] private GunDataView weaponWorldUi;
    [SerializeField] private Transform targetLookAtTransform;

    private GunAPI currentGunApi;
    private GunDataView currentWeaponUi;

    private void Start()
    {
        if (targetLookAtTransform == null)
            targetLookAtTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (currentWeaponUi != null)
        {
            currentWeaponUi.transform.LookAt(targetLookAtTransform.position);
            currentWeaponUi.transform.position = currentGunApi.transform.position;
        }
    }

    public void OnGunSelected(GunAPI gun)
    {
        currentGunApi = gun;
        currentWeaponUi = Instantiate(weaponWorldUi, gun.transform.position, Quaternion.identity, null);

        currentWeaponUi.OnGunDataChange(gun.GunData);
    }

    public void OnGunDeselected()
    {
        Destroy(currentWeaponUi.gameObject);
        
        currentWeaponUi = null;
        currentGunApi = null;
    }
}
