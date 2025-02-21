using System.Collections;
using UnityEngine;

public class PerlinShake : MonoBehaviour
{
    [SerializeField] private Vector3 axisClamp;
    [SerializeField] private float defaultFrequency;
    [SerializeField] private float defaultPower;

    [Space]
    [SerializeField] private float divider;

    private float seed;

    private void Start()
    {
        seed = Random.value;
    }

    public void Shake(float time)
    {
        Shake(defaultPower * (time / divider), defaultFrequency);
    }

    public void Shake(float power, float frequency)
    {
        transform.localPosition = power * new Vector3(
            axisClamp.x * Mathf.PerlinNoise(seed, Time.time * frequency) - .5f,
            axisClamp.y * Mathf.PerlinNoise(seed + 1, Time.time * frequency) - .5f,
            axisClamp.z * Mathf.PerlinNoise(seed + 1, Time.time * frequency) - .5f
        );
    }

    //5, 20, 0.2f
    public IEnumerator StartShake(float shakeTime, float frequency, float power)
    {
        float seed = Random.value;
        float remainTime = shakeTime;

        while (remainTime > 0)
        {
            remainTime -= Time.deltaTime;

            transform.localPosition += (remainTime / shakeTime) * power * new Vector3 (
                axisClamp.x * Mathf.PerlinNoise(seed, Time.time * frequency) - .5f,
                axisClamp.y * Mathf.PerlinNoise(seed + 1, Time.time * frequency) - .5f,
                axisClamp.z * Mathf.PerlinNoise(seed + 1, Time.time * frequency) - .5f
            );

            yield return null;
        }
    }
}