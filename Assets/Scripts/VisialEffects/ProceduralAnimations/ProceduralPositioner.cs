using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class ProceduralPositioner : MonoBehaviour
{
    [SerializeField] private List<AnimationUnit> animationUnits = new List<AnimationUnit>();
    [SerializeField] public Transform instanceTransform;

    public UnityEvent<AnimationUnit> AnimationEnded;

    private int currentAnimationIndex;

    private AnimationUnit currentUnit;
    private IEnumerator currentAnim;

    private void Awake()
    {
        if (instanceTransform == null)
            instanceTransform = transform;
    }

    public AnimationUnit GetAnimation(string name) => animationUnits.Find(x => x.AnimationName == name);

    public AnimationUnit GetAnimation(int index) => animationUnits[currentAnimationIndex];

    public void SetAnimation(string animationName)
    {
        SetAnimation(animationUnits.FindIndex((unit) => unit.AnimationName == animationName));
    }

    public void SetAnimation(int index)
    {
        if (animationUnits.Count < index)
            throw new ArgumentException(nameof(index));
        
        StopAllCoroutines();

        currentAnimationIndex = index;
        currentUnit = animationUnits[currentAnimationIndex];

        currentAnim = MoveToPosition(currentUnit);
        StartCoroutine(currentAnim);
    }

    private IEnumerator MoveToPosition(AnimationUnit unit)
    {
        var currentTime = 0f;
        var animationTime = unit.TimeCurve.keys.Last().time;

        var startPosition = transform.localPosition;
        var startRotation = transform.localRotation;

        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;

            transform.localPosition = Vector3.LerpUnclamped (
                startPosition,
                unit.TargetTransform.localPosition,
                unit.TimeCurve.Evaluate(currentTime)
            );

            transform.localRotation = Quaternion.SlerpUnclamped (
                startRotation,
                unit.TargetTransform.localRotation,
                unit.TimeCurve.Evaluate(currentTime)
            );

            yield return null;
        }

        AnimationEnded?.Invoke(unit);
        currentAnim = null;
        currentUnit = null;
    }
}