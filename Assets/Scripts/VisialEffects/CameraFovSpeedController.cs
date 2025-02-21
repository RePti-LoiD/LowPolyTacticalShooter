using UnityEngine;
using System.Collections.Generic;

public class CameraFovSpeedController : MonoBehaviour
{
    [SerializeField] private List<CameraFovSpeedSettings> cameras;

    public void HorizontalSpeedChagedHandler(float horizontalSpeed)
    {
        foreach (var item in cameras)
        {
            item.camera.fieldOfView = Mathf.Lerp(
                item.camera.fieldOfView,
                item.defaultFov + Mathf.Clamp(horizontalSpeed / item.fovChangeDivider, 0, item.maxFovChange),
                Time.deltaTime * item.fovChangeDuration);
        }
    }
}