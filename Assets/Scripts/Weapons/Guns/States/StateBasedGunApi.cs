public class StateBasedGunApi : BaseGunAPI
{
    public enum GunState
    {
        Idle,
        Shoting,
        Aiming,
        Reloading,
        Modifying,
        Inspecting,
        Enabled,
        Disabled
    }

    private GunState currentState = GunState.Disabled;

    public override void EnableGun(ExternalDataForGun data)
    {
        SetState(GunState.Enabled);

        base.EnableGun(data);
    }

    public override void DisableGun()
    {
        SetState(GunState.Disabled);

        base.DisableGun();
    }

    public override void ShotStart()
    {
        if (currentState == GunState.Reloading || currentState == GunState.Modifying || currentState == GunState.Inspecting) return;

        base.ShotStart();
        SetState(GunState.Shoting);
    }

    public override void ShotStop()
    {
        if (currentState == GunState.Reloading || currentState == GunState.Modifying || currentState == GunState.Inspecting) return;

        SetState(GunState.Idle);
        base.ShotStop();
    }

    public override void AdditionalAction()
    {
        if (currentState == GunState.Modifying || currentState == GunState.Inspecting) return;
       
        print($"AAAAAASs: {currentState}");
        base.AdditionalAction();
        SetState(GunState.Aiming);
    }

    public override void AdditionalActionStop()
    {
        if (currentState == GunState.Modifying || currentState == GunState.Inspecting) return;

        SetState(GunState.Idle);
        base.AdditionalActionStop();
    }

    public override void Reload()
    {
        if (currentState == GunState.Modifying || currentState == GunState.Inspecting) return;
        
        SetState(GunState.Reloading);
        base.Reload();
    }

    public override void Inspect()
    {
        SetState(GunState.Inspecting);
        base.Inspect();
    }

    public override void Modify()
    {
        if (currentState == GunState.Reloading || currentState == GunState.Inspecting) return;
        
        SetState(GunState.Modifying);
        base.Modify();
    }

    public override void ModifyCanceled()
    {
        if (currentState == GunState.Reloading || currentState == GunState.Inspecting) return;

        SetState(GunState.Idle);
        base.ModifyCanceled();
    }

    public override void OnAnimationEnded(AnimationUnit animationUnit)
    {
        base.OnAnimationEnded(animationUnit);

        if (animationUnit.AnimationName == "HoldDown")
            SetState(GunState.Disabled);

        if (animationUnit.AnimationName == "Default")
            SetState(GunState.Idle);
    }

    public void SetState(GunState targetState)
    {
        currentState = (targetState, currentState) switch
        {
            (GunState.Shoting, GunState.Aiming or GunState.Idle or GunState.Enabled) => targetState,
            (GunState.Aiming, GunState.Idle or GunState.Enabled or GunState.Shoting or GunState.Disabled) => targetState,
            (GunState.Reloading, GunState.Idle or GunState.Enabled or GunState.Shoting or GunState.Aiming) => targetState,
            (GunState.Modifying, GunState.Idle or GunState.Enabled or GunState.Shoting or GunState.Aiming) => targetState,
            (GunState.Inspecting, GunState.Idle or GunState.Enabled or GunState.Shoting) => targetState,

            (GunState.Disabled, GunState.Idle or GunState.Enabled or GunState.Shoting or GunState.Aiming or GunState.Inspecting) => targetState,
            (GunState.Enabled, GunState.Disabled) => targetState,
            (GunState.Idle, GunState.Shoting or GunState.Aiming or GunState.Reloading or GunState.Inspecting or GunState.Enabled or GunState.Modifying) => targetState,

            _ => currentState
        };
        print($"Current state: {currentState}");
    }
}