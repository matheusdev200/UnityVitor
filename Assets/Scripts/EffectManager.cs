using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public bool active = true;
    public GameObject[] effectObjects;
    List<ParticleSystem> vaporEffects = new List<ParticleSystem>();
    public float effectTimer;

    void Awake()
    {
        for (int i = 0; i < effectObjects.Length; i++) 
        {
            vaporEffects.Add(effectObjects[i].GetComponentInChildren<ParticleSystem>());
        }
    }

    void Start()
    {
        StartCoroutine(ActivateVaporRoutine());
    }
    IEnumerator ActivateVaporRoutine()
    {
        for (int i = 0; i < vaporEffects.Count; i++)
        {
            //faz a divisão analógica e pega quanto ficou no resto
            if (i % 2 == 0)//o resto da divisão vale zero
            {
                //esse index é par
                vaporEffects[i].Play();//aciona a partícula e dá play
            }
        }

        yield return new WaitForSeconds(effectTimer);

        for (int i = 0; i < vaporEffects.Count; i++)
        {
            //faz a divisão analógica e pega quanto ficou no resto
            if (i % 2 != 0)//o resto da divisão é diferente de zero
            {
                //esse index é ímpar
                vaporEffects[i].Play();//aciona a partícula e dá play
            }
        }

        yield return new WaitForSeconds(effectTimer);

        if (active)
        {
            StartCoroutine(ActivateVaporRoutine());
        }
    }
}