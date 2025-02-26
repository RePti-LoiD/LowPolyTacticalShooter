using System.Collections;
using System.Linq;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public IEnumerator RotateProjectile(float targetAngle, float time)
    {
        var currentTime = 0f;
        var startRotation = transform.localEulerAngles;
        var localTargetAngle = transform.localEulerAngles.x + targetAngle;

        while (currentTime / time < 1)
        {
            currentTime += Time.deltaTime;

            transform.localEulerAngles = Vector3.Lerp(
                startRotation,
                new Vector3(
                    localTargetAngle,
                    transform.localEulerAngles.y,
                    transform.localEulerAngles.z
                ),
                currentTime / time
            );

            yield return null;
        }
    }

    public IEnumerator RotateProjectile(AnimationCurve curve)
    {
        float maxTime = curve.keys.Last().time;

        float prevAngle = 0f;
        float currentT = 0f;
        float prevT = 0f;

        print(maxTime);

        while (currentT + 0.01f < maxTime)
        {
            print("aSS");
            currentT += Time.deltaTime;

            float dy = curve.Evaluate(currentT) - curve.Evaluate(prevT);
            float dx = currentT - prevT;

            float angle = Mathf.Rad2Deg * Mathf.Atan(dy / dx);

            transform.localEulerAngles += new Vector3(Mathf.Abs(angle - prevAngle), 0);

            prevAngle = angle;
            prevT = currentT;

            yield return null;
        }
    }
}