using UnityEngine;

public abstract class MouseMovementControllable : MonoBehaviour
{
    public abstract void OnCameraMove(Vector2 mouseInputs);
}