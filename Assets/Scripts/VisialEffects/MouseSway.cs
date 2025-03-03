using UnityEngine;

public class MouseSway : MonoBehaviour
{
    [SerializeField] private Transform targetTransoform;

    [Space]
    [SerializeField] private Vector2 clampVector;
    [SerializeField] private float objectReturnSpeed;
    [SerializeField] private float multiplier;

    public void OnMouseMove(Vector2 mousePosition)
    {
        targetTransoform.localPosition = Vector3.Lerp (
            transform.localPosition,
            new Vector3 (
                mousePosition.x, 
                mousePosition.y
            ) * multiplier, 
            Time.deltaTime * objectReturnSpeed
        );
    }
}