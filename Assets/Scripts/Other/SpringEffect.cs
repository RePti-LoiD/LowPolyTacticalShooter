using UnityEngine;

public static class SpringEffect
{
    public static float Spring(float from, float to, float time)
    {
        time = Mathf.Clamp01(time);
        time = (Mathf.Sin(time * Mathf.PI * (.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + (1.2f * (1f - time)));

        return from + (to - from) * time;
    }

    public static Vector3 Spring(Vector3 from, Vector3 to, float time)
    {
        return new Vector3(Spring(from.x, to.x, time), Spring(from.y, to.y, time), Spring(from.z, to.z, time));
    }
}