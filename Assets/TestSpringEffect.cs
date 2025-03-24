using System.Collections;
using UnityEngine;

public class TestSpringEffect : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float time;

    private Vector3 instantPostition;

    private void Start()
    {
        float angleX = Mathf.Acos(0);
        float angleY = Mathf.Asin(1);

        print((angleX * Mathf.Rad2Deg, angleY * Mathf.Rad2Deg));

        instantPostition = transform.position;

        StartCoroutine(LaunchSpring());
    }

    private IEnumerator LaunchSpring()
    {
        var currentTime = 0f;

        while (time > currentTime)
        {
            currentTime += Time.deltaTime;

            transform.position = SpringEffect.Spring(instantPostition, targetPosition, currentTime / time);
            
            yield return null;
        }
    }
}