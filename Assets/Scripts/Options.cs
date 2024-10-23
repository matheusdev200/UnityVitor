using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Options : MonoBehaviour //contador de FPS + o singleton pra ele funcionar em toda cena
{
    public static Options instance;
    [Header("Taxa de Quadros-Alvo")]
    [Range(30, 60)] public int targetFPS;

    TextMeshProUGUI fpsText;
    float fpsNumber;

    public Coroutine framerateRoutine;
    bool countingFPS;
    void OnEnable()
    {
        EventsMaster.OnChangeFPS += CheckFPSEvent;
    }
    void OnDisable()
    {
        EventsMaster.OnChangeFPS -= CheckFPSEvent;
    }
    void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        if (instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        fpsText.enabled = false;
    }
    void Start()
    {
        //InvokeRepeating("CalculateFramerate", 0f, 1f);
        //framerateRoutine = StartCoroutine(CalculateFramerate());
        SetTargetFPS();
        DontDestroyOnLoad(transform.parent);
    }
    IEnumerator CalculateFramerate()
    {
        fpsNumber = 1f / Time.deltaTime;
        fpsText.text = "[ FPS: " + fpsNumber.ToString("00") + " ]";
        yield return new WaitForSeconds(1f);
        framerateRoutine = StartCoroutine(CalculateFramerate());
    }
    #region coisas para interface :)
    public void CheckFPSEvent()
    {
        if (countingFPS)
            StopFramerateCounter();
        else
            StartFramerateCounter();

        countingFPS = !countingFPS;//pega a bool e inverte ela
    }
    public void StopFramerateCounter()
    {
        fpsText.enabled = false;
        StopCoroutine(framerateRoutine);
    }
    public void StartFramerateCounter()
    {
        fpsText.enabled = true;
        framerateRoutine = StartCoroutine(CalculateFramerate());
    }
    #endregion

    #region Configurar FPS
    public void SetTargetFPS()
    {
        Application.targetFrameRate = targetFPS;
    }
    #endregion
}
/*

public class Fade : MonoBehaviour
{
    public static Fade instance;

    public Color firstColor;
    public Color secondColor;

    public Image myImage; //sempre acrescentar o componente de fade onde EXISTE um IMAGE
    [Range(0.4f, 2.5f)] public float fadeTime;
    [Range(0f, 3f)] public float visibleTime;
    [Range(0f, 3f)] public float waitTime;
    float t;

    public bool fadein;
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
        instance = this;
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
                EventsMaster.FadeOutStarted?.Invoke();
                while (t < fadeTime)
                {
                    t += Time.deltaTime;
                    myImage.color = Color.Lerp(firstColor, secondColor, t / fadeTime);
                    yield return null;
                }
                //yield return new WaitForSeconds(visibleTime * 2);
                EventsMaster.FadeOutEnded?.Invoke();
                yield return null;
            }
            //da segunda cor pra primeira cor
            if (!fadein)
            {
                EventsMaster.FadeInStarted?.Invoke();
                t = fadeTime;
                while (t > 0f)
                {
                    t -= Time.deltaTime;
                    myImage.color = Color.Lerp(firstColor, secondColor, t / fadeTime);
                    yield return null;
                }
                EventsMaster.FadeInEnded?.Invoke();
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

/*Fade Example
     * IEnumerator FadeTagText()
    {
        bool active = true;
        if (active)
        {
            if (fadeText)
            {
                while (count < visibleTime)
                {
                    count += Time.deltaTime;
                    tagText[tagpanel].alpha = Mathf.Lerp(0f, 1f, count / visibleTime);
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
                fadeText = false;
            }
            if (!fadeText)
            {
                while (count > 0f)
                {
                    count -= Time.deltaTime;
                    tagText[tagpanel].alpha = Mathf.Lerp(0f, 1f, count / visibleTime);
                    yield return null;
                }
            }
        }
    }
     
     */