using System;
using UnityEngine;

[Serializable]
class CameraFovSpeedSettings
{
    public Camera camera;
    public float defaultFov;
    public float maxFovChange;
    public float fovChangeDivider;
    public float fovChangeDuration;
}