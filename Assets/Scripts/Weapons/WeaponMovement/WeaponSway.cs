using UnityEngine;

namespace WeaponBehaviour
{
    public class WeaponSway : RootPositioning
    {
        [Header("Input")]
        
        [Header("Position")]
        [SerializeField] private float amount = 0.02f;
        [SerializeField] private float maxAmount = 0.06f;
        [SerializeField] private float smoothAmount = 6f;

        [Header("Rotation")]
        [SerializeField] private float rotationAmount = 2f;
        [SerializeField] private float maxRotationAmount = 6f;

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


        private void Update()
        {
            MoveSway();

            RotationSway(rotationAmount, maxRotationAmount);
        }

        private void MoveSway()
        {
            transform.localPosition = Vector3.Lerp (
                transform.localPosition,
                (Vector3)(mouseInputs * amount).Clamp(new Vector3(maxAmount, maxAmount)) + rootPosition, 
                Time.deltaTime * smoothAmount
            );
        }

        private void RotationSway(float rotationAmount, float maxRotationAmount)
        {
            Vector3 targetRotation = (mouseInputs * rotationAmount).Clamp(new Vector2(maxRotationAmount, maxRotationAmount));

            transform.localRotation = Quaternion.Slerp (
                transform.localRotation,
                Quaternion.Euler (
                    new Vector3(
                        AxisX ? -targetRotation.y : 0,
                        AxisY ? targetRotation.x : 0,
                        AxisZ ? -targetRotation.x : 0
                    )
                ) * rootRotation,
                Time.deltaTime * smoothRotation
            );
        }
    }
}