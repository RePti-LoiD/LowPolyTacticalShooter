using UnityEngine;

public class CameraBob : TransformComposable
{
    [SerializeField] private float bobAmount;
    [SerializeField] private float bobFrequency;
    [Space]
    [SerializeField] private bool stabilize = true;
    [SerializeField] private bool executeInUpdate = false;

    [Space]
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
        if (executeInUpdate)
        {
            if (stabilize)
                transform.LookAt(stabilizationTracker.position);

            transform.localPosition = Vector3.Lerp(transform.localPosition, GetPosition(transform.localPosition), Time.deltaTime * returnSpeed);
        }
    }

    public override Vector3 GetPosition(Vector3 prevPosition) =>
        currentPosition;

    public override Quaternion GetRotation(Quaternion prevRotation)
    {
        if (stabilize)
            return Quaternion.LookRotation(stabilizationTracker.position);
        else
            return Quaternion.identity;
    }
}
