using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour //gerente de paineis
{
    bool confirmQuitGame = false;

    Coroutine currentCloseGameRoutine;

    #region FPS
    public void ShowFPS()//método que faz o botão chamar o evento
    {
        //EventsMaster.OnChangeFPS?.Invoke();//jeito de fazer com evento
        Options.instance.CheckFPSEvent();//jeito de fazer sem evento
    }
    #endregion
    #region Paineis
    public void TogglePanel(GameObject panelObject) //alterna ativacao de objetos
    {
        bool objectActive = panelObject.activeInHierarchy;
        panelObject.SetActive(!objectActive);
    }
    public void OpenPanel(GameObject panelObject)//so ativa
    {
        panelObject.SetActive(true);
    }
    public void ClosePanel(GameObject panelObject)//so desativa
    {
        panelObject.SetActive(false);
    }
    #endregion
    #region Quit Game
    public void FecharOJogo()
    {
        currentCloseGameRoutine = StartCoroutine(CloseGameRoutine());
    }
    public void ConfirmQuitGame(bool confirmation)
    {
        if (confirmation == true)
        {
            confirmQuitGame = true;
        }
        else
        {
            StopCoroutine(currentCloseGameRoutine);
            confirmQuitGame = false;
        }
    }
    IEnumerator CloseGameRoutine()
    {
        yield return new WaitUntil(() => confirmQuitGame == true);
        Application.Quit();
    }
    #endregion
}
