using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [SerializeField] private string gunName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private string caliberName;

    [Space]
    [SerializeField] private Sprite gunSprite;
    [SerializeField] private Color gunColor;

    [Space]
    [SerializeField] private int ammoCount;
    [SerializeField] private int remainAmmo;

    public string GunName { get => gunName; set => gunName = value; }
    public string Description { get => description; set => description = value; }
    public string CaliberName { get => caliberName; set => caliberName = value; }
    
    public Sprite GunSprite { get => gunSprite; set => gunSprite = value; }
    public Color GunColor { get => gunColor; set => gunColor = value; }

    [Obsolete]
    public int MaxAmmoCount { get => ammoCount; set => ammoCount = value; }
    [Obsolete]
    public int MaxRemainAmmo { get => remainAmmo; set => remainAmmo = value; }
}