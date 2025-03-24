using UnityEngine;
using UnityEngine.Events;

public class BaseGunAPI : GunAPI
{
    [SerializeField] private GameObject children;

    [SerializeField] public ProceduralPositioner proceduralPositioner;
    [SerializeField] protected TransformCompositor transformCompositor;
    [SerializeField] protected RecoilRotationSender cameraRecoil;

    [Header("Events")]
    [SerializeField] private UnityEvent shotStart;
    [SerializeField] private UnityEvent shotStop;
    [SerializeField] private UnityEvent additionalAction;
    [SerializeField] private UnityEvent additionalActionStop;
    [SerializeField] private UnityEvent inspect;
    [SerializeField] private UnityEvent reload;
    [SerializeField] private UnityEvent modify;
    [SerializeField] private UnityEvent modifyCanceled;
    
    [Space]
    [SerializeField] public UnityEvent gunEnabled;
    [SerializeField] public UnityEvent gunDisabled;

    [SerializeField] private ProjectileThrower projectileThrower;


    protected ExternalDataForGun LastData;

    public override void DisableGun()
    {
        DisableComponents();
    }

    public void DisableComponents()
    {
        gunDisabled?.Invoke();
        StopAllCoroutines();

        if (proceduralPositioner != null)
            proceduralPositioner.enabled = false;

        if (transformCompositor != null)
            transformCompositor.enabled = false;

        if (cameraRecoil != null)
        {
            cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
        }
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        if (proceduralPositioner != null)
            proceduralPositioner.enabled = true;

        if (transformCompositor != null)
            transformCompositor.enabled = true;

        LastData = data;
        gunEnabled?.Invoke();

        if (cameraRecoil != null)
            cameraRecoil.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);

        if (projectileThrower != null)
            projectileThrower.SetPool(data.ProjectilePool);
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

    public virtual void OnAnimationEnded(AnimationUnit animationUnit)
    {
        if (animationUnit.AnimationName == "HoldDown")
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

    public override void Modify() =>
        modify?.Invoke();

    public override void ModifyCanceled() =>
        modifyCanceled?.Invoke();
}
