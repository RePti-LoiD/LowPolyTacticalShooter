using UnityEngine;

public class RecoilRotationReceiver : TransformComposable
{
    [SerializeField] protected float returnTime;
    [SerializeField] protected float snappines;

    protected Vector3 current;
    protected Vector3 target;

    public void RotateObject(Vector3 vector)
    {
        print($"Rotate target: {vector}");

        target += vector;
    }

    private void CalculateRotation()
    {
        target = Vector3.Lerp(target, Vector3.zero, returnTime * Time.deltaTime);
        current = Vector3.Lerp(current, target, snappines * Time.fixedDeltaTime);
    }

    public override Vector3 GetPosition(Vector3 prevPosition) => 
        Vector3.zero;

    public override Quaternion GetRotation(Quaternion prevRotation)
    {
        CalculateRotation();

        return Quaternion.Euler(current);
    }
}