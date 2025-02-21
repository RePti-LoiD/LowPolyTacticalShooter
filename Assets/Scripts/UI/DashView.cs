using UnityEngine;
using UnityEngine.UI;

public class DashView : MonoBehaviour
{
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private Image dashImage;
    [SerializeField] private float fillAmountSpeed;
    [SerializeField] private float alphaFadingTime;
    [SerializeField] private float maxShowTime;

    private float segmentSizeValue;

    private float lastDataReceiveTime = 0;
    private float targetFillAmount = 0f;

    private void Awake()
    {
        segmentSizeValue = 1 / (float) movementSettings.DashCount;
    }

    private void Update()
    {
        if (Time.time - lastDataReceiveTime > maxShowTime)
            dashImage.CrossFadeAlpha(0, alphaFadingTime, false);

        dashImage.fillAmount = Mathf.Lerp(dashImage.fillAmount, targetFillAmount, fillAmountSpeed * Time.deltaTime);
    }

    public void OnDash(DashEventArgs e)
    {
        lastDataReceiveTime = Time.time;
        targetFillAmount = segmentSizeValue * e.CurrentDashCount;

        dashImage.CrossFadeAlpha(1, alphaFadingTime, false);
    }
}
