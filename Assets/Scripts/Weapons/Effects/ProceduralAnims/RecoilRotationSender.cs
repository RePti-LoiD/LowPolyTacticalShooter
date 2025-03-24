using UnityEngine;

public class RecoilRotationSender : MonoBehaviour
{
    [SerializeField] private RecoilRotationData recoilRotationData;
    [SerializeField] public Vector3Event OnRecoil;

    private float multiplier = 1f;

    public RecoilRotationData GetRecoilRotationData() =>
        recoilRotationData;

    public void SetMultiplier(float multiplier) => 
        this.multiplier = multiplier;

    public void SendRotation()
    {
        OnRecoil?.Invoke (
            new Vector3 (
                Random.Range(recoilRotationData.rotation.x, recoilRotationData.targetRotation.x),
                Random.Range(recoilRotationData.rotation.y, recoilRotationData.targetRotation.y),
                Random.Range(recoilRotationData.rotation.z, recoilRotationData.targetRotation.z)
            ) * multiplier
        );
    }

    public void SendRotation(Vector3 rotation)
    {
        OnRecoil?.Invoke(rotation);
    }
}
