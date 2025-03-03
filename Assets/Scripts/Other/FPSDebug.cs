using TMPro;
using UnityEngine;

public class FPSDebug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float refrashTime;

    private float lastRefresh;
    private float currentFps;
    
    private void Update()
    {
        currentFps = (int)(1f / Time.unscaledDeltaTime);
        
        if (Time.time - lastRefresh > refrashTime)
        {
            fpsText.text = $"{(currentFps)} FPS";
            lastRefresh = Time.time;
        }
    }
}
