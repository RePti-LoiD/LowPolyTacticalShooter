using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private float crouchColliderHeight;

    private float defaultHeight;

    private void Awake()
    {
        defaultHeight = playerCollider.height;
    }

    public void OnCrouch()
    {
        print("Gay");
        playerCollider.height = crouchColliderHeight;
    }

    public void OnCrouchStop()
    {
        playerCollider.height = defaultHeight;
    }
}
