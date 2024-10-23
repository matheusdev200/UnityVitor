using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlatform : MonoBehaviour
{
    public bool moveUp;
    [SerializeField] Transform PontoA;
    [SerializeField] Transform PontoB;
    public float velocMovimento;
    float velocMovimentoCorrigida;

    Rigidbody myrb;

    #region coisas do freio
    public float porcentagemDeFreio;//o quão perto do pontoB eu preciso estar pra começar a freiar
    float velocidadeDecrescente;
    float distanciaDoPontoB;//ponto B é o ponto de cima
    #endregion

    [SerializeField] float tempoDeEspera = 1.5f;
    public bool repeteOMovimento = true;
    bool movimento = false;


    void Start()
    {
        myrb = GetComponent<Rigidbody>();
        StartCoroutine(PlataformaVerticalRoutine()); //manda a rotina começar
        //StopCoroutine(PlataformaVerticalRoutine()); //manda a rotina parar
    }
    void Update()
    {
        velocMovimentoCorrigida = velocMovimento * 10f;

        if (transform.position.y < PontoA.position.y)
        {
            moveUp = true;
        }
        if (transform.position.y > PontoB.position.y)
        {
            moveUp = false;
        }
    }
    void FixedUpdate()
    {
        //PlataformaVertical();
        //Debug.Log($"velocidade da plataforma é: <size=16>{myrb.velocity.y}</size>");
    }
    void PlataformaVertical()
    {
        velocMovimentoCorrigida = velocMovimento * 10f;
        if (transform.position.y < PontoA.position.y)
        {
            moveUp = true;
        }
        if (transform.position.y > PontoB.position.y)
        {
            moveUp = false;
        }
        //if (moveUp)
        //{
        //    //enquanto estiver subindo

        //    distanciaDoPontoB = Vector3.Distance(transform.position, PontoB.position);//do ponto onde começa para o ponto onde termina
        //    //Debug.Log($"a distância do ponto B vale: <size=14>{distanciaDoPontoB}</size>");

        //    if (distanciaDoPontoB <= porcentagemDeFreio) //se está chegando na distância onde a gente começa a freiar;
        //    {
        //        ////parte / todo = %
        //        //float porcentagem = 1 - distanciaDoPontoB / porcentagemDeFreio; //distância atual dividida pela distância total
        //        //que tem que usar pra parar
        //        //Debug.Log($"a porcentagem vale: <size=14>{porcentagem}</size>");
        //        velocidadeDecrescente = velocMovimentoCorrigida * 0.5f; //freia 
        //        myrb.velocity = Vector3.up * velocidadeDecrescente * Time.deltaTime;
        //    }
        //    else//enquanto estiver descendo
        //    {
        //        myrb.velocity = Vector3.up * velocMovimentoCorrigida * Time.deltaTime;
        //    }
        //}
        //else //enquanto estiver descendo
        //{
        //    myrb.velocity = Vector3.down * velocMovimentoCorrigida * Time.deltaTime;
        //}
    }

    IEnumerator PlataformaVerticalRoutine()//Coroutine ("rotina de código")
    {
        movimento = true;
        if (movimento)
        {
            yield return new WaitForSeconds(tempoDeEspera);//tempo de espera entre a ida e a volta

            while (moveUp && Vector3.Distance(transform.position, PontoA.position) > 0f)
            {
                //Debug.Log("subindo :)");
                myrb.velocity = Vector3.up * velocMovimentoCorrigida * Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            while (!moveUp && Vector3.Distance(transform.position, PontoB.position) > 0f)
            {
                //Debug.Log("descendo :)");
                myrb.velocity = Vector3.down * velocMovimentoCorrigida * Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
        if (repeteOMovimento)
        {
            //pergunta se precisa fazer de novo
            //se precisar, começa a rotina de novo
            StartCoroutine(PlataformaVerticalRoutine());
        }
    }
}