using UnityEngine;

public class AnimatorTriggerSetter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;

    public void SetTrigger()
    {
        animator.SetTrigger(triggerName);
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
