using UnityEngine;
using UnityEngine.Events;

public class BaseGunAPI : GunAPI
{
    [SerializeField] private GameObject children;

    [SerializeField] public ProceduralPositioner proceduralPositioner;
    [SerializeField] protected TransformCompositor transformCompositor;

    [Header("Events")]
    [SerializeField] private UnityEvent shotStart;
    [SerializeField] private UnityEvent shotStop;
    [SerializeField] private UnityEvent additionalAction;
    [SerializeField] private UnityEvent additionalActionStop;
    [SerializeField] private UnityEvent inspect;
    [SerializeField] private UnityEvent reload;
    
    [Space]
    [SerializeField] public UnityEvent gunEnabled;
    [SerializeField] public UnityEvent gunDisabled;

    protected ExternalDataForGun LastData;

    public override void DisableGun()
    {
        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;
        
        if (transformCompositor != null)
            transformCompositor.enabled = false;

        base.DisableGun();
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        if (proceduralPositioner != null)
            proceduralPositioner.enabled = true;

        if (transformCompositor != null)
            transformCompositor.enabled = true;

        LastData = data;
        gunEnabled?.Invoke();
        
        //children.SetActive(true);
    }

    public override void ForceStop()
    {
        gunDisabled?.Invoke();

        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;

        if (transformCompositor != null)
            transformCompositor.enabled = false;

        Disabled?.Invoke(this);
    }

    public override void ShotStart() =>
        shotStart?.Invoke();

    public override void ShotStop() =>
        shotStop?.Invoke();

    public override void AdditionalAction() =>
        additionalAction?.Invoke();

    public override void Inspect() =>
        inspect?.Invoke();

    public override void Reload() =>
        reload?.Invoke();

    public override void AdditionalActionStop() =>
        additionalActionStop?.Invoke();
}
