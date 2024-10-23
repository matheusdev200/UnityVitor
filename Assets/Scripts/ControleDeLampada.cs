using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeLampada : MonoBehaviour
{
    public float taxaDeAtualizacao = 0.2f;
    public float distanciaPraLigar = 5f;

    public Transform playerTransform;

    Light lampada;

    void Start()
    {
        lampada = GetComponent<Light>();
        StartCoroutine(VerificaDistanciaDoPlayer());
    }

    IEnumerator VerificaDistanciaDoPlayer()
    {
        //Debug.Log(Vector3.Distance(transform.position, playerTransform.position));
        if (Vector3.Distance(transform.position, playerTransform.position) <= distanciaPraLigar)
        {
            lampada.enabled = true;
        }
        else
        {
            lampada.enabled = false;
        }

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(VerificaDistanciaDoPlayer());//liga o teste de novo
    }
}