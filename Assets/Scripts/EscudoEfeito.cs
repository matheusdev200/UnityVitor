using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class EscudoEfeito : MonoBehaviour
{
    [ColorUsage(true, true)] public Color32 corApagado;
    [ColorUsage(true, true)] public Color32 corAtivado;

    [ColorUsage(true, true)] Color32 corDoEfeito;

    public float tempoDeAcender = 0.8f;
    public float tempoDeApagar = 0.35f;

    public float duracaoTotalDoEfeito = 1f;

    public MeshRenderer materialDoEfeito;

    public Animator anim;

    //bool ligaOEscudo = true;

    void Start()
    {
        //StartCoroutine(AlternaEfeitoDoEscudo(ligaOEscudo));
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ligaOEscudo = !ligaOEscudo;
        //    StartCoroutine(AlternaEfeitoDoEscudo(ligaOEscudo));
        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Ativar Escudo");
            //StartCoroutine(ControleDoEscudo());
        }
    }
    IEnumerator ControleDoEscudo()
    {
        //yield return new WaitForSeconds(0.5f);
        //suspende a coroutine até o FINAL da coroutine que a suspensão pediu pra começar
        yield return StartCoroutine(AlternaEfeitoDoEscudo(true)); // acende o escudo
        //yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(AlternaEfeitoDoEscudo(false)); // apaga o escudo
    }
    IEnumerator AlternaEfeitoDoEscudo(bool ativando = false)
    {
        bool active = true;
        float tempoDoEfeito = 0f;
        if (active)
        {
            if (ativando)
            {
                while (tempoDoEfeito < duracaoTotalDoEfeito)
                {
                    tempoDoEfeito += Time.deltaTime;
                    corDoEfeito = Color.Lerp(corApagado/*"preto"*/, corAtivado/*"vermelho"*/, tempoDoEfeito / duracaoTotalDoEfeito);
                    materialDoEfeito.material.SetColor("_Cor_Da_Energia", corDoEfeito);
                    yield return null;
                }
                yield return null;
            }
            if (!ativando)
            {
                tempoDoEfeito = 0f;
                float duracaoDeApagar = duracaoTotalDoEfeito / 2f;
                while (tempoDoEfeito < duracaoDeApagar)
                {
                    tempoDoEfeito += Time.deltaTime;
                    corDoEfeito = Color.Lerp(corAtivado/*"vermelho"*/, corApagado/*"preto"*/, tempoDoEfeito / duracaoDeApagar);
                    materialDoEfeito.material.SetColor("_Cor_Da_Energia", corDoEfeito);
                    yield return null;
                }
            }
        }
        //SetEndFade();
    }
}


/*
 using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeElement : MonoBehaviour
{
    [Header("Cores")]
    public Color firstColor;
    public Color secondColor;

    [Header("Tempos")]
    [Range(0.4f, 2.5f)] public float fadeTime;
    [Range(0f, 3f)] public float visibleTime;
    [Range(0f, 3f)] public float waitTime;

    [Header("Sentido do Fade")]
    public bool fadein;
    
    float t;
    Image myImage;
    void Awake()
    {
        myImage = GetComponent<Image>();
    }
    void OnEnable()
    {
        myImage.color = firstColor;
    }
    void Start()
    {
        StartCoroutine(Fading());
    }
    IEnumerator Fading()
    {
        yield return new WaitForSeconds(waitTime);
        bool active = true;
        if (active)
        {
            //da primeira cor pra segunda cor
            if (fadein)
            {

                while (t < fadeTime)
                {
                    t += Time.deltaTime;
                    myImage.color = Color.Lerp(firstColor, secondColor, t / fadeTime);
                    yield return null;
                }
                //yield return new WaitForSeconds(visibleTime * 2);

                yield return null;
            }
            //da segunda cor pra primeira cor
            if (!fadein)
            {

                t = fadeTime;
                while (t > 0f)
                {
                    t -= Time.deltaTime;
                    myImage.color = Color.Lerp(firstColor, secondColor, t / fadeTime);
                    yield return null;
                }

            }
        }
        //SetEndFade();
    }

    public void SetFadeInOut(bool f)
    {
        fadein = f;
        StartCoroutine(Fading());
    }
    public void SetEndFade()
    {
        //Debug.Log("Trying to call end fade");
        //FadeEnded?.Invoke();
        //dá um grito avisando a quem quer que
        //esteja ouvindo que esse evento foi chamado.
    }
}

 */