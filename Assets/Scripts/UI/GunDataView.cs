using System;
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

    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorParameterName;

    [Space]
    [SerializeField] private GunColorElement[] gunColorElements;


    public void OnGunDataChange(RuntimeGunData runtimeGunData)
    {    
        if (gunNameText != null)
            gunNameText.text = runtimeGunData.GunData.GunName;
        if (gunAmmoText != null)
            gunAmmoText.text = runtimeGunData.GunAmmo.CurrentAmmoCount.ToString();
        if (gunAmmoRemainText != null)
            gunAmmoRemainText.text = runtimeGunData.GunAmmo.RemainAmmoCount.ToString();

        if (gunCaliberText != null)
            gunCaliberText.text = runtimeGunData.GunData.CaliberName;

        if (gunDescriptionText != null)
            gunDescriptionText.text = runtimeGunData.GunData.Description;  
        
        if (gunAmmoFillAmount != null)
            gunAmmoFillAmount.fillAmount = (float) runtimeGunData.GunAmmo.CurrentAmmoCount / runtimeGunData.GunAmmo.GunAmmoData.MaxAmmoCount;


        if (gunIcon != null)
            gunIcon.sprite = runtimeGunData.GunData.GunSprite;

        if (animator != null)
            animator.SetTrigger(animatorParameterName);

        SetGunColorToElements(runtimeGunData.GunData.GunColor, gunColorElements);
    }

    public void SetGunColorToElements(Color gunColor, GunColorElement[] elements)
    {
        foreach (var element in elements)
            element.TargetSprite.color = new Color(gunColor.r, gunColor.g, gunColor.b, element.Alpha);
    }
}

[Serializable]
public class GunColorElement
{
    public Image TargetSprite;
    public float Alpha;
}