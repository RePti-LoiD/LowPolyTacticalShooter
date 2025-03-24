using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class ProceduralPositioner : TransformComposable
{
    [SerializeField] private List<AnimationUnit> animationUnits = new List<AnimationUnit>();
    [SerializeField] public Transform instanceTransform;
    
    [Space]
    [SerializeField] private bool executeOnObject = true;
    [SerializeField] private bool setFirstPositionAsDefault = false;

    public UnityEvent<AnimationUnit> AnimationEnded;

    private int currentAnimationIndex;

    private AnimationUnit currentUnit;
    private IEnumerator currentAnim;

    private Vector3 currentPosition;
    private Quaternion currentRotation;

    private float weight = 1f;

    private void Awake()
    {
        currentPosition = Vector3.zero;
        currentRotation = Quaternion.identity;

        if (instanceTransform == null)
            instanceTransform = transform;
    }

    private void Start()
    {
        if (setFirstPositionAsDefault)
            SetAnimation(0);
    }

    public void SetWeight(float weight) => this.weight = Mathf.Clamp(weight, 0, 1f);

    public AnimationUnit GetAnimation(string name) => animationUnits.Find(x => x.AnimationName == name);

    public AnimationUnit GetAnimation(int index) => animationUnits[currentAnimationIndex];

    public void SetAnimation(string animationName) =>
        SetAnimation(animationUnits.FindIndex((unit) => unit.AnimationName == animationName));

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

            currentPosition = Vector3.LerpUnclamped (
                startPosition,
                unit.TargetTransform.localPosition,
                unit.TimeCurve.Evaluate(currentTime)
            );

            currentRotation = Quaternion.SlerpUnclamped (
                startRotation,
                unit.TargetTransform.localRotation,
                unit.TimeCurve.Evaluate(currentTime)
            );

            if (executeOnObject)
            {
                transform.localPosition = currentPosition;
                transform.localRotation = currentRotation;
            }

            yield return null;
        }

        AnimationEnded?.Invoke(unit);
        currentAnim = null;
        currentUnit = null;
    }

    public override Vector3 GetPosition(Vector3 prevPosition) =>
        Vector3.Slerp(Vector3.zero, currentPosition, weight);

    public override Quaternion GetRotation(Quaternion prevRotation) =>
        Quaternion.Slerp(Quaternion.identity, currentRotation, weight);
}