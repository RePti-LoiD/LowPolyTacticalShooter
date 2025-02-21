using UnityEngine;

public class ArmRecoilRotation : MonoBehaviour
{
    [SerializeField] private FistsAPI fistsAPI;

    public void LeftArmRecoil()
    {
        fistsAPI.leftHitRotationSender.SendRotation();
    }

    public void RightArmRecoil()
    {
        fistsAPI.rightHitRotationSender.SendRotation();
    }
}
