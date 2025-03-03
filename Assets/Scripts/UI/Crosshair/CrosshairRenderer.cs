using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairRenderer : MonoBehaviour
{
    [SerializeField] private CrosshairSettings crosshairSettings;

    [SerializeField] private Transform dotTransform;
    [SerializeField] private Transform crosshairTransform;

    private Dictionary<RectTransform, float> crosshair = new();
    private RectTransform dot;

    private void Start()
    {
        InitializeCrosshair();
    }

    public void EnableCrosshair() =>
        CrosshairSetActive(true);

    public void DisableCrosshair() =>
        CrosshairSetActive(false);

    private void CrosshairSetActive(bool state)
    {
        foreach (Transform t in dotTransform)
            t.gameObject.SetActive(state);

        if (dot != null)
            dot.gameObject.SetActive(state);
    }

    public void InitializeCrosshair()
    {
        DrawCrosshairPoint();
        DrawCrosshairLines();

        SetCrosshairOffset();
    }

    private void DrawCrosshairPoint()
    {
        dot = CreateCrosshairPart(
            name: "point",
            parent: dotTransform,
            localPosition: Vector3.zero,
            localRotation: Vector3.zero,
            localScale: Vector3.one,
            component: typeof(Image),
            length: crosshairSettings.DotRadius,
            width: crosshairSettings.DotRadius,
            color: crosshairSettings.Color,
            sprite: crosshairSettings.DotSprite
        );
    }

    private void SetCrosshairOffset()
    {
        transform.localPosition = new Vector3(crosshairSettings.CrosshairOffestX, crosshairSettings.CrosshairOffestY);
    }

    private void DrawCrosshairLines()
    {
        float lineAngle = (2 * Mathf.PI) / crosshairSettings.LineCount;

        float currentAngle = crosshairSettings.LineCount % 2 == 0 ? 0 : lineAngle / 4;
        for (var i = 0; i < crosshairSettings.LineCount; i++)
        {
            var crosshairRect = CreateCrosshairPart(
                name: $"line {i}",
                parent: crosshairTransform,
                localPosition: new Vector2(
                    Mathf.Cos(currentAngle),
                    Mathf.Sin(currentAngle)
                ) * crosshairSettings.Gap,
                localRotation: new Vector3(0, 0, RadToDeg(currentAngle)),
                localScale: Vector3.one,
                component: typeof(Image),
                length: crosshairSettings.Length,
                width: crosshairSettings.Width,
                color: crosshairSettings.Color
            );

            crosshair[crosshairRect] = currentAngle;

            currentAngle += lineAngle;
        }
    }

    private RectTransform CreateCrosshairPart (
        string name, 
        Transform parent, 
        Vector3 localPosition, 
        Vector3 localRotation, 
        Vector3 localScale, 
        Type component,
        float length,
        float width,
        Color color,
        Sprite sprite = null
    )
    {
        var gameObj = new GameObject(name, component);

        gameObj.transform.SetParent(parent, false);
        gameObj.transform.localPosition = localPosition;
        gameObj.transform.localScale = localScale;
        gameObj.transform.localEulerAngles = localRotation;
        gameObj.GetComponent<Image>().color = color;

        if (sprite != null)
            gameObj.GetComponent<Image>().sprite = sprite;

        var rect = gameObj.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(length, width);

        return rect;
    }

    public void OnVelocityChanged(float velocity)
    {
        foreach ((var rect, var angle) in crosshair)
            rect.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * (crosshairSettings.Gap + velocity) * 5f;
    }

    public float RadToDeg(float rad) =>
        rad * Mathf.Rad2Deg;

    public float DegToRad(float deg) =>
        deg * Mathf.Deg2Rad;
}