using UnityEngine;

[CreateAssetMenu(fileName = "CrosshairSettings", menuName = "Scriptable Objects/CrosshairSettings")]
public class CrosshairSettings : ScriptableObject
{
    public float Length;
    public float Width;
    public float Gap;   

    public bool DotInCenter;
    public float DotRadius;

    public float CrosshairOffestX;
    public float CrosshairOffestY;

    public Color Color;

    public bool TShaped;

    [Range(2f, 7f)]
    public int LineCount;
}