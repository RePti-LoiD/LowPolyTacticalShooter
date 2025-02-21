using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private float fillAmountSpeed;
    [SerializeField] private float alphaFadingTime;
    [SerializeField] private float maxShowTime;

    private float lastDataReceiveTime = 0;
    private float targetFillAmount = 0f;

    private void Update()
    {
        if (Time.time - lastDataReceiveTime > maxShowTime)
            healthImage.CrossFadeAlpha(0, alphaFadingTime, false);

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, targetFillAmount, fillAmountSpeed * Time.deltaTime);
    }

    public void OnHealthChanged(HealthEventArgs e)
    {
        lastDataReceiveTime = Time.time;
        targetFillAmount = e.PercentOfHealth;

        healthImage.CrossFadeAlpha(1, alphaFadingTime, false);
    }
}
