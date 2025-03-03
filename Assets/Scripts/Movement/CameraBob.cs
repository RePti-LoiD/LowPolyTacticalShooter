using UnityEngine;

public class CameraBob : TransformComposable
{
    [SerializeField] private float bobAmount;
    [SerializeField] private float bobFrequency;
    [SerializeField] private bool stabilize;
    [SerializeField] private float stabilizeAmount;
    [SerializeField] private float returnSpeed;

    [SerializeField] private Transform stabilizationTracker;

    private float Cos(float time) => Mathf.Cos(time);
    private float Sin(float time) => Mathf.Sin(time);

    private Vector3 currentPosition;

    public void OnMove(Vector2 inputs)
    {   
        currentPosition = Vector3.Lerp (
            currentPosition, 
            inputs != Vector2.zero ? new Vector3 ( 
                Cos(Time.time * bobFrequency) - (bobAmount / 2),
                Mathf.Abs(Sin(Time.time * bobFrequency)) - (bobAmount / 2)
            ) * bobAmount : Vector3.zero, 
            Time.deltaTime * returnSpeed
        );
    }

    private void Update()
    {
        if (stabilize)
        { 
            transform.localRotation = Quaternion.LookRotation(stabilizationTracker.forward * stabilizeAmount);
            Debug.DrawRay(transform.position, stabilizationTracker.forward * stabilizeAmount, Color.red);
        }
    }

    public override Vector3 GetPosition(Vector3 prevPosition) =>
        currentPosition;

    public override Quaternion GetRotation(Quaternion prevRotation)
    {
        if (stabilize)
            return Quaternion.LookRotation(stabilizationTracker.forward * stabilizeAmount);
        else
            return Quaternion.identity;
    }
}
