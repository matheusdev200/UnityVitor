using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenManager : MonoBehaviour
{
    public GameObject target;
    public Vector2[] targetPositions;
    public Vector3[] targetScales;
    public Vector3[] targetRotations;
    public float targetTimer;

    public void AnimateTargetPosition(int position)
    {
        LeanTween.move(target, targetPositions[position], targetTimer).setEase(LeanTweenType.easeInOutQuint);
    }
    public void AnimateTargetScale(int scale)
    {
        LeanTween.scale(target, targetScales[scale], targetTimer).setEase(LeanTweenType.easeInOutQuint);
    }
    public void AnimateTargetRotation(int rotation)
    {
        LeanTween.rotateAround(target, Vector3.forward, targetRotations[rotation].z, targetTimer).setEase(LeanTweenType.easeInOutQuint);
    }
    public void StartSequenceOfAnimations()
    {
        StartCoroutine(SequenceOfThingsRoutine());
    }
    IEnumerator SequenceOfThingsRoutine()
    {
        AnimateTargetPosition(1);
        yield return new WaitForSeconds(targetTimer);
        AnimateTargetScale(1);
        yield return new WaitForSeconds(targetTimer);
        AnimateTargetScale(0);
        yield return new WaitForSeconds(targetTimer);
        AnimateTargetPosition(0);
        yield return new WaitForSeconds(targetTimer);
        AnimateTargetRotation(1);
    }
}