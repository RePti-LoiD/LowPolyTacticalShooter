using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hitmarker : MonoBehaviour
{
    [SerializeField] private Image[] hitmarkerParts;

    [Header("Colors")]
    [SerializeField] private Color startHitColor;
    [SerializeField] private Color finalHitColor;

    [Space]
    [SerializeField] private Color startFatalHitColor;
    [SerializeField] private Color finalFatalHitColor;

    private float currentOffset = 25;

    public void OnDisable()
    {
        StopAllCoroutines();
        
        foreach (var hitmarkerPart in hitmarkerParts)
            hitmarkerPart.gameObject.SetActive(false);
    }

    public void FatalHit()
    {
        AnimateHit(hitmarkerParts, currentOffset, 50, 25, 35, Color.red, 1f);
    }

    public void Hit()
    {
        AnimateHit(new HitmarkerAnimationData()
        {
            HitmarkerParts = hitmarkerParts,
            StartOffset = 10,
            FinalOffset = 45,
            StartColor = startHitColor,
            FinishColor = finalHitColor,
            StartHeight = 25,
            FinalHeight = 35,
            Time = 0.1f
        });
    }

    public void AnimateHit(Image[] hitmarkerParts, float startOffset, float finalOffset, float height, float finalHeight,  Color color, float time)
    {
        AnimateHit(new HitmarkerAnimationData()
        {
            HitmarkerParts = hitmarkerParts,
            StartOffset = startOffset,
            FinalOffset = finalOffset,
            StartColor = color,
            FinishColor = color,
            StartHeight = height,
            FinalHeight = finalHeight,
            Time = time
        });
    }

    public void AnimateHit(HitmarkerAnimationData animationData)
    {
        StartCoroutine(Animate(animationData));
    }

    private IEnumerator Animate(HitmarkerAnimationData animationData)
    {
        float currentTime = 0f;

        foreach (var hitmarkerPart in animationData.HitmarkerParts)
            hitmarkerPart.gameObject.SetActive(true);

        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime / animationData.Time;

            foreach (var hitmarkerPart in animationData.HitmarkerParts)
            {
                float angle = hitmarkerPart.rectTransform.localRotation.z * Mathf.Deg2Rad;

                var currentColor = Color.Lerp(animationData.StartColor, animationData.FinishColor, currentTime);
                var currentPosition = hitmarkerPart.rectTransform.localPosition.normalized * Mathf.Lerp(animationData.StartOffset, animationData.FinalOffset, currentTime);
                var currentSize = new Vector2(hitmarkerPart.rectTransform.rect.width, Mathf.Lerp(animationData.StartHeight, animationData.FinalHeight, currentTime));

                hitmarkerPart.rectTransform.sizeDelta = currentSize;
                hitmarkerPart.rectTransform.localPosition = currentPosition;
                hitmarkerPart.color = currentColor;
            }

            yield return null;
        }

        foreach (var hitmarkerPart in animationData.HitmarkerParts)
            hitmarkerPart.gameObject.SetActive(false);
    }

    private Vector2 CalcOffset(float angle) =>
        new Vector2(MathF.Cos(angle), MathF.Sin(angle));
}

public class HitmarkerAnimationData
{
    public Image[] HitmarkerParts { get; set; }
    public float Time { get; set; }
    public float StartOffset { get; set; }
    public float FinalOffset { get; set; }
    public float StartHeight { get; set; }
    public float FinalHeight { get; set; }
    public Color StartColor { get; set; }
    public Color FinishColor { get; set; }
}