using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private new Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void PlayAnimation(string animationName)
    {
        animation.Play();
    }
}
