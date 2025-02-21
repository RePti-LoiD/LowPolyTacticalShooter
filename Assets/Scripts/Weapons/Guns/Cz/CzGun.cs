using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CzGun : WeaponInputLayer
{
    [SerializeField] private float damage;
    [SerializeField] private float minRicochetAngle;
    [SerializeField] private Vector3ArrayEvent OnShotTargetTransform;

    private Transform raycastOrigin;

    private void Start()
    {
        raycastOrigin = Camera.main.transform;
    }

    public override void OnShot()
    {
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
            else
            {
                var reflect = hitInfo.point + Vector3.Reflect(raycastOrigin.forward, hitInfo.normal) * 100;
                float angle = Vector3.Angle(raycastOrigin.forward, reflect);

                if (angle < minRicochetAngle)
                    raycastPath.Add(reflect);
            }

            OnShotTargetTransform?.Invoke(raycastPath.ToArray());
        }
        else
        {
            OnShotTargetTransform?.Invoke(new Vector3[] { raycastOrigin.position + raycastOrigin.forward * 100 });
        }
    }
}