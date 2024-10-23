using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTecnoBall : MonoBehaviour
{
    bool encolher = false;
    bool apertandoBotao = false;
    bool inputCallback;
    [SerializeField] LayerMask layerDeCenario;
    public float raioDaBola;
    public float refreshRate = 0.1f;
    public Vector3 ajusteDeAltura;

    void Update()
    {
        ControleEncolherJogador();
    }
    void ControleEncolherJogador()
    {
        inputCallback = Input.GetKey(KeyCode.Escape);
        if (inputCallback)
        {
            apertandoBotao = true;
            encolher = true;
            StartCoroutine(CheckBallSize());//lá ele 
        }
        if (!inputCallback)
        {
            apertandoBotao = false;
        }
    }
    IEnumerator CheckBallSize()
    {
        Debug.Log("Verificando altura da bola");
        //transform.position da bolinha + o ajuste da altura => raio da bola encolhida => camada pro cenario
        if (Physics.OverlapSphere(transform.position + ajusteDeAltura, raioDaBola, layerDeCenario).Length > 0)
        {
            encolher = true;
            Debug.Log("encolhe");

        }
        else//se tiver espaco pra voltar ao normal
        {
            if (!apertandoBotao) //verifica se o botao ainda esta apertado
            {
                encolher = false;//se nao tiver, volta o tamanho normal
                Debug.Log("desencolhe");
            }
        }
        yield return new WaitForSeconds(refreshRate);
        if (encolher) //se estiver encolhido, continua testando se tem espaco pra voltar pro tamanho normal
        {
            StartCoroutine(CheckBallSize());
        }
    }





    void OnDrawGizmos()//remover ou comentar quando estiver pronto
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + ajusteDeAltura, raioDaBola);
    }
}