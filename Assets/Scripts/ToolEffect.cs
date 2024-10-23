using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolEffect : MonoBehaviour
{
    public float particleTimer = 1f;
    void OnEnable()
    {
        StartCoroutine(EffectTimerRoutine());
    }
    IEnumerator EffectTimerRoutine()
    {
        float timer = 0f;

        while (timer < particleTimer) //jeito alternativo de fazer um timer
        {
            timer += Time.deltaTime;
            yield return null;
        }

        PoolingManager.ReturnObjectToPool(gameObject);
    }
}