using UnityEngine;
using UnityEngine.Events;

public abstract class ShotType : MonoBehaviour
{
    public UnityEvent OnShot;

    public virtual void OnShotStart() { }
    public virtual void OnShotStop() { }
}