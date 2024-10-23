using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneDirector : MonoBehaviour
{
    public PlayableDirector director; //o diretor que controla o filme
    /*
    - adicionar exemplo de resposta a signal
    - criar depois exemplo de extensão de trilha (o componente de clipe personalizado)
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))//espera apertar ENTER
        {
            director.Play();
        }
    }
}