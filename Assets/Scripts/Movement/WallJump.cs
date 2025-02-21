using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Vector3 forcePower;
    [SerializeField] private float jumpForce;

    private Vector3 hitNormal;
    private bool hitWall;

    public void OnJump()
    {
        if (!hitWall) return;

        movement.ZeroLinearVelocity();
        movement.ZeroMoveVector();

        var targetVector = hitNormal + Vector3.up; 

        movement.AddLinearVelocity(new Vector3
            (
                targetVector.x * forcePower.x,
                targetVector.y * forcePower.y,
                targetVector.z * forcePower.z
            )
        );
        movement.SetJumpVelocity(jumpForce);
    }

    private void OnTriggerStay(Collider other)
    {
        var direction = Vector3.Normalize(other.transform.position - transform.position);
        hitWall = true;

        if (Physics.Raycast(transform.position, direction * 100, out RaycastHit hitInfo))
            hitNormal = hitInfo.normal;
    }

    private void OnTriggerExit(Collider other)
    {
        hitWall = false;
    }
}
