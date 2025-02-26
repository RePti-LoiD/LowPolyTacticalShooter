using UnityEngine;

public class RecoilRotationReceiver : TransformComposable
{
    [SerializeField] protected float returnTime;
    [SerializeField] protected float snappines;

    protected Vector3 current;
    protected Vector3 target;

    public void RotateObject(Vector3 vector)
    {
        target += vector;
    }

    private void CalculateRotation()
    {
        target = Vector3.Lerp(target, Vector3.zero, returnTime * Time.deltaTime);
        current = Vector3.Lerp(current, target, snappines * Time.fixedDeltaTime);
    }

    public override Vector3 GetPosition() => 
        Vector3.zero;

    public override Quaternion GetRotation()
    {
        CalculateRotation();

        return Quaternion.Euler(current);
    }
}