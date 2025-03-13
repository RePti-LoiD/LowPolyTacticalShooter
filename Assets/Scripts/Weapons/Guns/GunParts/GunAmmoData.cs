using UnityEngine;

[CreateAssetMenu(fileName = "gunAmmoData", menuName = "Scriptable Objects/GunParts/gunAmmoData")]
public class GunAmmoData : ScriptableObject
{
    [SerializeField] private int maxAmmoCount;
    [SerializeField] private int maxRemainAmmoCount;
    [SerializeField] private int shotPerMinute;

    [Space]
    [SerializeField] private int bulletDamage;
    [SerializeField] private int bulletSpeed;

    [Space]
    [SerializeField] private string caliberName;

    public int MaxAmmoCount { get => maxAmmoCount; }
    public int MaxRemainAmmoCount { get => maxRemainAmmoCount; }
    public int ShotPerMinute { get => shotPerMinute; }
    public int BulletDamage { get => bulletDamage; }
    public int BulletSpeed { get => bulletSpeed; }

    public string CaliberName { get => caliberName; }
}