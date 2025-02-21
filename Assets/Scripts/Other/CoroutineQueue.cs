using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue : MonoBehaviour
{
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();

    private void Start()
    {
        StartCoroutine(CoroutineNavigator());
    }

    private IEnumerator CoroutineNavigator()
    {
        while (true)
        {
            while (coroutineQueue.Count > 0)
                yield return StartCoroutine(coroutineQueue.Dequeue());

            yield return null;
        }
    }

    public void AddCoroutineToQueue(IEnumerator coroutine) =>
        coroutineQueue.Enqueue(coroutine);
}