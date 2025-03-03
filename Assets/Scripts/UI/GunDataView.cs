using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunDataView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunNameText;
    [SerializeField] private TextMeshProUGUI gunDescriptionText;
    [SerializeField] private TextMeshProUGUI gunAmmoText;
    [SerializeField] private TextMeshProUGUI gunAmmoRemainText;
    [SerializeField] private TextMeshProUGUI gunCaliberText;

    [SerializeField] private Image gunIcon;
    [SerializeField] private Image gunAmmoFillAmount;

    public void OnGunDataChange(RuntimeGunData runtimeGunData)
    {    
        gunNameText.text = runtimeGunData.GunData.GunName;
        gunAmmoText.text = runtimeGunData.CurrentAmmoCount.ToString();
        gunAmmoRemainText.text = runtimeGunData.RemainAmmoCount.ToString();

        if (gunCaliberText != null)
            gunCaliberText.text = runtimeGunData.GunData.CaliberName;

        if (gunDescriptionText != null)
            gunDescriptionText.text = runtimeGunData.GunData.Description;  
        
        if (gunAmmoFillAmount != null)
            gunAmmoFillAmount.fillAmount = runtimeGunData.CurrentAmmoCount / runtimeGunData.GunData.MaxAmmoCount;

        if (gunIcon != null)
            gunIcon.sprite = runtimeGunData.GunData.GunSprite;
    }
}
