using Unity.VisualScripting;
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

        private Vector3 currentPosition;
        private Quaternion currentRotation;

        private float globalSwayAmount = 1f;

        public void SetGlobalSwayAmount(float amount) =>
            globalSwayAmount = amount;

        public void OnMouseInput(Vector2 newMouseVector)
        {
            mouseInputs = newMouseVector;
        }

        public void ApplyTransform()
        {
            CalcPositionSway();
            CalcRotationSway();

            transform.localPosition = currentPosition;
            transform.localRotation = currentRotation;
        }

        private void CalcPositionSway()
        {
            currentPosition = Vector3.Lerp (
                currentPosition,
                (Vector3)(mouseInputs * amount).Clamp(maxAmount) * globalSwayAmount,
                Time.deltaTime * smoothAmount
            );
        }

        private void CalcRotationSway()
        {
            Vector3 targetRotation = (mouseInputs * rotationAmount).Clamp(maxRotationAmount);

            currentRotation = Quaternion.Slerp (
                currentRotation,
                Quaternion.Euler (
                    new Vector3 (
                        AxisX ? -targetRotation.y : 0,
                        AxisY ? targetRotation.x : 0,
                        AxisZ ? -targetRotation.x : 0
                    ) * globalSwayAmount
                ),
                Time.deltaTime * smoothRotation
            );
        }

        public override Vector3 GetPosition(Vector3 prevPosition)
        {
            CalcPositionSway();
            return currentPosition;
        }

        public override Quaternion GetRotation(Quaternion prevRotation)
        {
            CalcRotationSway();
            return currentRotation;
        }
    }
}