using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Railgun : WeaponInputLayer
{
    [SerializeField] private float damage;
    [SerializeField] private Vector3ArrayEvent OnShotTargetTransform;
    [SerializeField] private RailgunAPI railgunAPI;

    [SerializeField] private Vector2 positionalRecoilClamp;
    [SerializeField] private float jumpVelocityMultiplier;

    private Transform raycastOrigin;

    private float currentDamage;

    private void Start()
    {
        raycastOrigin = Camera.main.transform;
    }

    public void OnShot(float time)
    {
        currentDamage = time * damage;

        railgunAPI.AddMovementAdditionalVelocity (
            time * new Vector3 
            (
                -raycastOrigin.transform.forward.x * positionalRecoilClamp.x, 0,
                -raycastOrigin.transform.forward.z * positionalRecoilClamp.y
            )
        );
        railgunAPI.SetJumpVelocity(time * jumpVelocityMultiplier);

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out RaycastHit hitInfo))
        {
            var raycastPath = new List<Vector3>() { hitInfo.point };

            if (hitInfo.transform.gameObject.TryGetComponent(out Coin currentCoin))
            {
                FindObjectsByType<Coin>(FindObjectsSortMode.None).ToList().ForEach(x =>
                {
                    if (x != currentCoin)
                        raycastPath.Add(x.transform.position);

                    x.StopAllCoroutines();
                    Destroy(x.gameObject);
                });

                raycastPath.Add(FindAnyObjectByType<Target>().transform.position);
            }

            OnShotTargetTransform?.Invoke(raycastPath.ToArray());
        }
        else
        {
            OnShotTargetTransform?.Invoke(new Vector3[] { raycastOrigin.position + raycastOrigin.forward * 100 });
        }
    }
}