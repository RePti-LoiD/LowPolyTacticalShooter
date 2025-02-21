using TMPro;
using UnityEngine;

public class DebugPlayerSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    
    public void DrawSpeed(float speed)
    {
        speedText.text = $"{Mathf.Round(speed)} m/s";
    }
}
