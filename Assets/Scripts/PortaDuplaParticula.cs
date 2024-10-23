using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaDuplaParticula : MonoBehaviour
{
    //junta todas as referencias necessarias pras animacoes
    public GameObject particulaprefab;//prefab de particula
    public GameObject nextAnimator;//o proximo objeto da sequencia

    public void SpawnParticle()
    {
        Instantiate(particulaprefab, transform.position, Quaternion.identity);
    }
    public void StartOtherAnimation()//manda ativar o proximo objeto da sequencia
    {
        nextAnimator.GetComponent<Animator>().SetTrigger("Abrindo");//vai no animator do proximo objeto e manda
                                                                    //comecar a animacao que era pra tocar
    }
}