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
    [SerializeField] private Image divider;

    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorParameterName;


    public void OnGunDataChange(RuntimeGunData runtimeGunData)
    {    
        gunNameText.text = runtimeGunData.GunData.GunName;
        gunAmmoText.text = runtimeGunData.CurrentAmmoCount.ToString();
        gunAmmoRemainText.text = runtimeGunData.RemainAmmoCount.ToString();

        divider.color = runtimeGunData.GunData.GunColor;

        if (gunCaliberText != null)
            gunCaliberText.text = runtimeGunData.GunData.CaliberName;

        if (gunDescriptionText != null)
            gunDescriptionText.text = runtimeGunData.GunData.Description;  
        
        if (gunAmmoFillAmount != null)
        {
            gunAmmoFillAmount.fillAmount = (float) runtimeGunData.CurrentAmmoCount / runtimeGunData.GunData.MaxAmmoCount;
            gunAmmoFillAmount.color = new Color(runtimeGunData.GunData.GunColor.r, runtimeGunData.GunData.GunColor.g, runtimeGunData.GunData.GunColor.b, 0.28f);
        }

        if (gunIcon != null)
            gunIcon.sprite = runtimeGunData.GunData.GunSprite;

        if (animator != null)
            animator.SetTrigger(animatorParameterName);
    }
}
