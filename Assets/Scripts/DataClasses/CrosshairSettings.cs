using UnityEngine;

[CreateAssetMenu(fileName = "CrosshairSettings", menuName = "Scriptable Objects/CrosshairSettings")]
public class CrosshairSettings : ScriptableObject
{
    public float Length;
    public float Width;
    public float Gap;   

    public bool DotInCenter;

    public Color Color;

    public bool TShaped;

    [Range(2f, 7f)]
    public int LineCount;
}