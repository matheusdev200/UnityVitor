using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//folha com todos os eventos (EventMaster)
//quem usa evento -> todos os outros scripts
public class EventosDeExemplo
{
    //"abreviacao" de Delegate
    public static Action OnFazEvento1;
    public static Action<string> OnFazEvento2;
}

public class QuemRespondeEventoExemplo : MonoBehaviour
{
    //a etapa de cadastrar ao evento para observar ele
    //o método que responde ao evento
    private void OnEnable()
    {
        EventosDeExemplo.OnFazEvento1 += MetodoDeResposta1;
    }
    private void OnDisable()
    {
        EventosDeExemplo.OnFazEvento1 -= MetodoDeResposta1;
    }
    private void Start()
    {
        EventosDeExemplo.OnFazEvento2?.Invoke("eventinho XD");
    }
    void MetodoDeResposta1()
    {
        Debug.Log("Respondi ao evento.");
    }
}

public class QuemUsaEvento : MonoBehaviour
{
    private void OnEnable()
    {
        EventosDeExemplo.OnFazEvento2 += MetodoDeRespostaComString;
    }
    private void OnDisable()
    {
        EventosDeExemplo.OnFazEvento2 -= MetodoDeRespostaComString;
    }
    private void Start()
    {
        if (EventosDeExemplo.OnFazEvento1 != null) //-> vira a ? do Invoke.
        {
            EventosDeExemplo.OnFazEvento1();
        }
        EventosDeExemplo.OnFazEvento1?.Invoke(/*poe os parametros aqui dentro se tiver*/);
    }
    void MetodoDeRespostaComString(string mensagem)
    {
        Debug.Log($"Respondi ao evento com.{mensagem}");
    }
}