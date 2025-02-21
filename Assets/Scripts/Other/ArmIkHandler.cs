using UnityEngine;

public class ArmIkHandler : MonoBehaviour
{
    [SerializeField] private ArmsIkAPI armsIkAPI;

    public void PassGunTarget(GunAPI gun)
    {
        armsIkAPI.SetArmIkTargets(gun.ArmsIkAPI.LeftHandTargetTransform, gun.ArmsIkAPI.RightHandTargetTransform);
    }
}