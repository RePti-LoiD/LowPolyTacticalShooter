using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [SerializeField] private string gunName;
    [SerializeField] private string description;
    [SerializeField] private int ammoCount;
    [SerializeField] private int remainAmmo;

    public string GunName { get => gunName; set => gunName = value; }
    public string Description { get => description; set => description = value; }

    [Obsolete]
    public int AmmoCount { get => ammoCount; set => ammoCount = value; }
    [Obsolete]
    public int RemainAmmo { get => remainAmmo; set => remainAmmo = value; }
}