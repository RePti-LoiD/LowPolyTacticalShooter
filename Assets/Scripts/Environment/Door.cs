using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableData doorInteractableData;

    [Space]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float animationTime;

    public InteractableData GetData() =>
        doorInteractableData;

    public void Interact(object sender, EventArgs args)
    {
        
    }

    public void Select()
    {

    }

    public void Unselect()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();

        StartCoroutine(AnimatePosition(targetTransform, targetTransform.localPosition, endPosition, animationTime));
    }

    public void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();

        StartCoroutine(AnimatePosition(targetTransform, targetTransform.localPosition, startPosition, animationTime));
    }

    public IEnumerator AnimatePosition(Transform transform, Vector3 startPosition, Vector3 targetPosition, float time)
    {
        var currentTime = 0f;

        while (currentTime / time < 1)
        {
            currentTime += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, currentTime / time);

            yield return null;
        }
    }
}