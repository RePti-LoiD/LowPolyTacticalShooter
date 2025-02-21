using UnityEngine;

public class MouseSway : MonoBehaviour
{
    [SerializeField] private Vector2 clampVector;
    [SerializeField] private float objectReturnSpeed;
    [SerializeField] private float multiplier;

    public void OnMouseMove(Vector2 mousePosition)
    {
        transform.localPosition = Vector3.Lerp (
            transform.localPosition,
            new Vector3 (
                mousePosition.x, 
                mousePosition.y
            ) * multiplier, 
            Time.deltaTime * objectReturnSpeed
        );
    }
}