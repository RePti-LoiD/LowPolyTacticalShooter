using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RuntimeGunData), true)]
public class idk : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/idk")]
    public static void ShowExample()
    {
        idk wnd = GetWindow<idk>();
        wnd.titleContent = new GUIContent("idk");
    }

    public void CreateGUI()
    {
        
    }
}
