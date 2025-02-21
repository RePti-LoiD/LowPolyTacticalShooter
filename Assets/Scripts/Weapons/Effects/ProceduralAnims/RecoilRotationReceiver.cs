using UnityEngine;

public class RecoilRotationReceiver : MonoBehaviour
{
    [SerializeField] protected float returnTime;
    [SerializeField] protected float snappines;

    protected Vector3 current;
    protected Vector3 target;

    private void Update()
    {
        Rotate();
    }

    public void RotateObject(Vector3 vector)
    {
        target += vector;
    }

    private void Rotate()
    {
        target = Vector3.Lerp(target, Vector3.zero, returnTime * Time.deltaTime);
        current = Vector3.Lerp(current, target, snappines * Time.fixedDeltaTime);
        gameObject.transform.localEulerAngles = current;
    }
}