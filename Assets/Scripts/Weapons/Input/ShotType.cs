using UnityEngine;
using UnityEngine.Events;

public abstract class ShotType : MonoBehaviour
{
    protected const int Minute = 60;

    public RuntimeGunData RuntimeGunData;

    public UnityEvent OnShot;

    public virtual void OnShotStart() { }
    public virtual void OnShotStop() { }
}