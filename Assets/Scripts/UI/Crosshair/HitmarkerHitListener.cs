using System;
using UnityEngine;
using UnityEngine.Events;

public class HitmarkerHitListener : MonoBehaviour
{
    [SerializeField] private Type[] targetTypes;

    [SerializeField] private UnityEvent commonHit;
    [SerializeField] private UnityEvent criticalHit;

    public void OnHit(Projectile projectile, GunAPI gun, GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out Target target))
        {
            commonHit?.Invoke();
        }
    }
}
