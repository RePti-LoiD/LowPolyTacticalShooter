using System;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    void Start()
    {
        ScreenCapture.CaptureScreenshot($"Assets/Screenshots/Screenshot_{DateTime.Now:dd.MM.yy_HH.mm.ss}.png", 1);
    }
}