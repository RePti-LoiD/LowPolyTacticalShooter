using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GunScopeData", menuName = "Scriptable Objects/GunParts/GunScopeData")]
public class GunScopeData : ScriptableObject
{
    [SerializeField] private string gunScopeName;
    [SerializeField] private float zoomCount;
    [SerializeField] private Image scopeIcon;

    [SerializeField] private float scopeFov;

    public string GunScopeName { get => gunScopeName; }
    public float ZoomCount { get => zoomCount; }
    public Image ScopeIcon { get => scopeIcon; }

    public float ScopeFov { get => scopeFov; set => scopeFov = value; }
}