using UnityEngine;

namespace WeaponBehaviour
{
    public class WeaponSway : TransformComposable
    {
        [Header("Input")]
        
        [Header("Position")]
        [SerializeField] private Vector3 amount = Vector3.zero;
        [SerializeField] private Vector3 maxAmount = Vector3.one;
        [SerializeField] private float smoothAmount = 6f;

        [Header("Rotation")]
        [SerializeField] private Vector3 rotationAmount = Vector3.zero;
        [SerializeField] private Vector3 maxRotationAmount = Vector3.one;

        [SerializeField] private float smoothRotation = 12f;

        [Header("AxisChecked")]
        [SerializeField] private bool AxisX = true;
        [SerializeField] private bool AxisY = true;
        [SerializeField] private bool AxisZ = true;

        private Vector2 mouseInputs;

        public void OnMouseInput(Vector2 newMouseVector)
        {
            mouseInputs = newMouseVector;
        }

        public void ApplyTransform()
        {
            transform.localRotation = GetRotation();
            transform.localPosition = GetPosition();
        }

        public override Vector3 GetPosition() => Vector3.Lerp(
            transform.localPosition,
            (Vector3)(mouseInputs * amount).Clamp(maxAmount),
            Time.deltaTime * smoothAmount
        );

        public override Quaternion GetRotation()
        {
            Vector3 targetRotation = (mouseInputs * rotationAmount).Clamp(maxRotationAmount);

            return Quaternion.Slerp(
                transform.localRotation,
                Quaternion.Euler(
                    new Vector3(
                        AxisX ? -targetRotation.y : 0,
                        AxisY ? targetRotation.x : 0,
                        AxisZ ? -targetRotation.x : 0
                    )
                ),
                Time.deltaTime * smoothRotation
            );
        }
    }
}