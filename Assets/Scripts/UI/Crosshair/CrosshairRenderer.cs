using System;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairRenderer : MonoBehaviour
{
    [SerializeField] private CrosshairSettings crosshairSettings;

    private RectTransform[] crosshair;

    private void Start()
    {
        InitializeCrosshair();
    }

    private void InitializeCrosshair()
    {
        crosshair = new RectTransform[crosshairSettings.LineCount];

        float lineAngle = (2 * Mathf.PI) / crosshairSettings.LineCount;

        float currentAngle = crosshairSettings.LineCount % 2 == 0 ? 0 : lineAngle / 4;
        for (var i = 0; i < crosshairSettings.LineCount; i++)
        {
            crosshair[i] = CreateLine (
                $"line {i}", 
                transform, 
                new Vector2 (
                    Mathf.Cos(currentAngle),
                    Mathf.Sin(currentAngle)
                ) * crosshairSettings.Gap,
                new Vector3 (0, 0, RadToDeg(currentAngle)),
                Vector3.one,
                typeof(Image),
                crosshairSettings.Length,
                crosshairSettings.Width,
                crosshairSettings.Color
            );

            currentAngle += lineAngle;
        }
    }

    private RectTransform CreateLine (
        string name, 
        Transform parent, 
        Vector3 localPosition, 
        Vector3 localRotation, 
        Vector3 localScale, 
        Type component,
        float length,
        float width,
        Color color
    )
    {
        var gameObj = new GameObject(name, component);

        gameObj.transform.SetParent(parent, false);
        gameObj.transform.localPosition = localPosition;
        gameObj.transform.localScale = localScale;
        gameObj.transform.localEulerAngles = localRotation;
        gameObj.GetComponent<Image>().color = color;

        var rect = gameObj.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(length, width);

        return rect;
    }

    public float RadToDeg(float rad) =>
        rad * (180 / MathF.PI);
}
