using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject firstButton; //botao que vai selecionar quando o menu de pause abrir

    public Transform openedPosition;
    public Transform closedPosition;
    public void PauseGame()
    {
        //Time.timeScale = 0f;
        gameIsPaused = true;//pausa
        StartCoroutine(AnimatePauseMenuRoutine(gameIsPaused));//comeca a rotina que vai animar o menu de pause
    }
    public void UnpauseGame()
    {
        //Time.timeScale = 1f;
        gameIsPaused = false;
        StartCoroutine(AnimatePauseMenuRoutine(gameIsPaused));//comeca a rotina que vai animar o menu de pause
    }
    public static void StartPauseGame()
    {
        gameIsPaused = !gameIsPaused; //alterna o pause com start
    }
    IEnumerator AnimatePauseMenuRoutine(bool opening)
    {
        if (opening) //se pausou
        {
            //roda a animação de mover pra tela
            //ativa a animação de abrir o painel
            //ativa os objetos do painel (botoes e o fundo)

            //la em cima nas bibliotecas, importa:
            //using UnityEngine.EventSystems;
            EventSystem.current.SetSelectedGameObject(firstButton);//quem ta selecionado agora
            yield return null;

        }
        else //se tirou do pause
        {
            //desativa os objetos do painel (botoes e o fundo)
            //ativa a animação de fechar o painel
            //roda a animação de mover pra fora da tela
            yield return null;
        }
    }
}