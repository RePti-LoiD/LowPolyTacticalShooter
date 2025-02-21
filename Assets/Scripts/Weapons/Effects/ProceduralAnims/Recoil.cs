using UnityEngine;

namespace WeaponBehaviour
{
    public class Recoil : MonoBehaviour
    {        
        [Header("Target Positions")]
        [SerializeField] protected Vector3 targetHipfireRecoil;

        [Header("Positions")]
        [SerializeField] protected Vector3 hipfireRecoil;

        [SerializeField] protected float returnTime;
        [SerializeField] protected float snappines;

        protected Vector3 current;
        protected Vector3 target;

        private void Update()
        {
            Rotate();
        }

        public void OnShot()
        {
            target += new Vector3(Random.Range(hipfireRecoil.x, targetHipfireRecoil.x),
                Random.Range(hipfireRecoil.y, targetHipfireRecoil.y),
                Random.Range(hipfireRecoil.z, targetHipfireRecoil.z));
        }

        private void Rotate()
        {
            target = Vector3.Lerp(target, Vector3.zero, returnTime * Time.deltaTime);
            current = Vector3.Slerp(current, target, snappines * Time.fixedDeltaTime);
            gameObject.transform.localPosition = current;
        }
    }
}