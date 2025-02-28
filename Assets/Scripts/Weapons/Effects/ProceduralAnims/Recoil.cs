using UnityEngine;

namespace WeaponBehaviour
{
    public class Recoil : TransformComposable
    {        
        [Header("Target Positions")]
        [SerializeField] protected Vector3 targetHipfireRecoil;

        [Header("Positions")]
        [SerializeField] protected Vector3 hipfireRecoil;

        [SerializeField] protected float returnTime;
        [SerializeField] protected float snappines;

        private float multiplier = 1f;

        protected Vector3 current;
        protected Vector3 target;

        public void SetMultiplier(float multiplier) => 
            this.multiplier = multiplier;

        public void OnShot()
        {
            target += new Vector3(Random.Range(hipfireRecoil.x, targetHipfireRecoil.x),
                Random.Range(hipfireRecoil.y, targetHipfireRecoil.y),
                Random.Range(hipfireRecoil.z, targetHipfireRecoil.z));
        }

        private void CalculateShotRecoil()
        {
            target = Vector3.Lerp(target, Vector3.zero, returnTime * Time.deltaTime) * multiplier;
            current = Vector3.Slerp(current, target, snappines * Time.deltaTime);
        }

        public override Vector3 GetPosition(Vector3 prevPosition)
        {
            CalculateShotRecoil();
            return current;
        }

        public override Quaternion GetRotation(Quaternion prevRotation) =>
            Quaternion.identity;
    }
}