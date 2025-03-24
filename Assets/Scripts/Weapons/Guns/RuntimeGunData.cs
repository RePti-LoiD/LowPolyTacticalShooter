using UnityEngine;
using UnityEngine.Events;

public class RuntimeGunData : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private GunAmmo gunAmmo;
    [SerializeField] private GunMuzzle gunMuzzle;
    [SerializeField] private GunScope gunScope;
    [SerializeField] private GunGrip gunGrip;

    [SerializeField] public UnityEvent<RuntimeGunData> GunDataChanged;

    public GunData GunData { get => gunData; }
    public GunAmmo GunAmmo { get => gunAmmo; }
    public GunMuzzle GunMuzzle { get => gunMuzzle; }
    public GunScope GunScope { get => gunScope; }
    public GunGrip GunGrip { get => gunGrip; }

    private void Start()
    {
        gunAmmo.GunAmmoDataChanged += (gunAmmo) => GunDataChanged?.Invoke(this);
    }

    public void OnScopeSet(GunScope gunScope)
    {
        this.gunScope = gunScope;
        GunDataChanged?.Invoke(this);
    }

    public void OnGunAmmoSet(GunAmmo gunAmmo)
    {
        this.gunAmmo = gunAmmo;
        GunDataChanged?.Invoke(this);
    }

    public void OnMuzzleSet(GunMuzzle gunMuzzle)
    {
        this.gunMuzzle = gunMuzzle;
        GunDataChanged?.Invoke(this);
    }

    public void OnGripSet(GunGrip gunGrip)
    {
        this.gunGrip = gunGrip;
        GunDataChanged?.Invoke(this);
    }
}

//#if UNITY_EDITOR

//[CustomEditor(typeof(RuntimeGunData))]
//public class RutimeGunDataEditorScript : Editor
//{
//    private RuntimeGunData gunData;
//    private GUISkin customGuiSkin;

//    private void OnEnable()
//    {
//        gunData = (RuntimeGunData)target;

//        customGuiSkin = Resources.Load<GUISkin>("GuiSkins/RuntimeGunDataSkin");
//        Debug.Log(customGuiSkin);
//    }

//    public override void OnInspectorGUI()
//    {
//        if (gunData.GunData != null)
//        {
//            GUI.skin = customGuiSkin;
            
//            GUILayout.Label (
//                gunData.GunData.GunName, 
//                GUILayout.MinWidth(50), 
//                GUILayout.MinHeight(50),
//                GUILayout.ExpandHeight(true)
//            );
            
//            GUI.skin = null;
//        }

//        base.OnInspectorGUI();
//    }
//}

//#endif