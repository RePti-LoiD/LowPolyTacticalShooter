using UnityEngine;
using UnityEngine.Events;
using UnityEditor;


public class RuntimeGunData : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private GunAmmo gunAmmo;
    [SerializeField] private GunMuzzle gunMuzzle;
    [SerializeField] private GunScope gunScope;
    [SerializeField] private GunGrip gunGrip;

    [SerializeField] private UnityEvent<RuntimeGunData> GunDataChanged;

    public GunData GunData { get => gunData; }
    public GunAmmo GunAmmo { get => gunAmmo; }
    public GunMuzzle GunMuzzle { get => gunMuzzle; }
    public GunScope GunScope { get => gunScope; }
    public GunGrip GunGrip { get => gunGrip; }

    private void Start()
    {
        if (gunAmmo == null)
            gunAmmo = GetComponentInChildren<GunAmmo>();
        if (gunMuzzle == null)
            gunMuzzle = GetComponentInChildren<GunMuzzle>();
        if (gunScope == null)
            gunScope = GetComponentInChildren<GunScope>();
        if (gunMuzzle == null)
            gunScope = GetComponentInChildren<GunScope>();

        gunAmmo.GunAmmoDataChanged += (gunAmmo) => GunDataChanged?.Invoke(this);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(RuntimeGunData))]
public class RutimeGunDataEditorScript : Editor
{
    private RuntimeGunData gunData;
    private GUISkin customGuiSkin;

    private void OnEnable()
    {
        gunData = (RuntimeGunData)target;

        customGuiSkin = Resources.Load<GUISkin>("GuiSkins/RuntimeGunDataSkin");
        Debug.Log(customGuiSkin);
    }

    public override void OnInspectorGUI()
    {
        if (gunData.GunData != null)
        {
            GUI.skin = customGuiSkin;
            
            GUILayout.Label (
                gunData.GunData.GunName, 
                GUILayout.MinWidth(50), 
                GUILayout.MinHeight(50),
                GUILayout.ExpandHeight(true)
            );
            
            GUI.skin = null;
        }

        base.OnInspectorGUI();
    }
}

#endif