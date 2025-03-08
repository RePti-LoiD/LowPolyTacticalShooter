using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GunApiQuickSetup : MonoBehaviour
{
    [Header("Global")]
    [SerializeField] public bool DestroySetupAfterInit = true;
    [SerializeField] public Transform childerHierarchyObject; 

    [Header("IK")]
    [SerializeField] public bool SetupArmIk = true;
    [SerializeField] public string RightHandIkTargetName = "TargetL";
    [SerializeField] public string LeftHandIkTargetName = "TargetR";
    
    [Header("Sound")]
    [SerializeField] public bool SetupAudio = true;

    [Header("Rigidbody")]
    [SerializeField] public bool SetupRigidbody = true;
    [SerializeField] public bool IsKinematic = true;
    [SerializeField] public Vector3 ColliderCenterPoint = new Vector3(0, 0.026f, 0.12f); 
    [SerializeField] public Vector3 ColliderSize = new Vector3(0.1f, 0.36f, 1);

    [Header("GunData")]
    [SerializeField] public bool SetupGunData = true;
    [SerializeField] public GunData GunData;

    [Header("TransformCompositor")]
    [SerializeField] public bool SetupTransformCompositor = true;
}

#if UNITY_EDITOR

[CustomEditor(typeof(GunApiQuickSetup))]
public class GunApiCreator : Editor
{
    private GunApiQuickSetup targetGunApi;

    private void OnEnable()
    {
        targetGunApi = (GunApiQuickSetup)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Initialize Gun"))
        {
            base.OnInspectorGUI();

            Debug.Log("Gun initializing");
            
            var gunApi = targetGunApi.AddComponent<BaseGunAPI>();
            targetGunApi.AddComponent<ProceduralPositioner>();
            
            if (targetGunApi.SetupGunData)
                SetupGunData(gunApi);

            if (targetGunApi.SetupArmIk)
                SetupGunIkAPI();
            
            if (targetGunApi.SetupAudio)
                SetupAudioSource();

            if (targetGunApi.SetupRigidbody)
                SetupRigidbodyAndCollider();

            if (targetGunApi.DestroySetupAfterInit)
                DestroyImmediate(targetGunApi);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SetupGunData(BaseGunAPI gunAPI)
    {
        var runtimeGunData = targetGunApi.AddComponent<RuntimeGunData>();
        
        gunAPI.GunData = runtimeGunData;
    }

    private void SetupGunIkAPI()
    {
        var gunIk = targetGunApi.gameObject.AddComponent<GunIkAPI>();

        var leftHand = Instantiate(new GameObject(targetGunApi.LeftHandIkTargetName), targetGunApi.transform);
        var rightHand = Instantiate(new GameObject(targetGunApi.RightHandIkTargetName), targetGunApi.transform);

        gunIk.LeftHandTargetTransform = leftHand.transform;
        gunIk.LeftHandTargetTransform = rightHand.transform;
    }

    private void SetupAudioSource()
    {
        var audioSource = targetGunApi.AddComponent<AudioSource>();
        var soundPlayer = targetGunApi.AddComponent<SoundPlayer>();

        soundPlayer.Source = audioSource;
    }

    private void SetupRigidbodyAndCollider()
    {
        var rb = targetGunApi.AddComponent<Rigidbody>();
        rb.isKinematic = targetGunApi.IsKinematic;
        
        var collider = targetGunApi.AddComponent<BoxCollider>();
        collider.size = targetGunApi.ColliderSize;
        collider.center = targetGunApi.ColliderCenterPoint;
    }
}

#endif