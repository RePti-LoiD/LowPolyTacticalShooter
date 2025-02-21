using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmsIkAPI : MonoBehaviour
{
    [SerializeField] private RigBuilder rigBuilder;
    [SerializeField] private TwoBoneIKConstraint leftArmConstraint;
    [SerializeField] private TwoBoneIKConstraint rightArmConstraint;

    public void SetArmIkTargets(Transform leftArmTarget, Transform rightArmTarget)
    {
        SetLeftArmIKTarget(leftArmTarget);
        SetRightArmIKTarget(rightArmTarget);
    }

    public void SetLeftArmIKTarget(Transform target)
    {
        leftArmConstraint.data.target = target;
        rigBuilder.Build();
    }
    
    public void SetRightArmIKTarget(Transform target)
    {
        rightArmConstraint.data.target = target;
        rigBuilder.Build();
    }
}