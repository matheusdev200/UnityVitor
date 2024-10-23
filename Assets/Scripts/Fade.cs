using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour //inicialmente so pra tela
{
    public static Fade instance;

    public Color firstColor;//cor 1
    public Color secondColor;//cor 2

    Image myImage;

    [Range(0.4f, 2.5f)] public float fadeTime;//velocidade da transicao

    [Range(0f, 3f)] public float visibleTime;//quanto tempo espera antes de fazer a segunda transicao

    [Range(0f, 3f)] public float waitTime;//quanto tempo espera pra fazer a primeira transicao

    float t = 0f;//variavel de rascunho pra calcular o tempo da transicao

    [Tooltip("FadeIn = true faz o fade in \n FadeIn = false faz o fade out")]
    public bool fadein = true;//bool pra controlar se transita da cor 1 pra cor 2 ou o contrario

    void Awake()
    {
        if (instance != this)
        {
            Destroy(instance);
        }
        instance = this;

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
    }

    public void SetFadeInOut(bool f)
    {
        fadein = f;
        StartCoroutine(Fading());
    }
}