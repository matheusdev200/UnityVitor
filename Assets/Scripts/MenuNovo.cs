using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuNovo : MonoBehaviour
{
    UIDocument menu;
    Button startGameButton;
    void Awake()
    {
        menu = GetComponent<UIDocument>();

        startGameButton = menu.rootVisualElement.Q("BotaoDePlay") as Button;
        //startGameButton = menu.rootVisualElement.Q<Button>("BotaoDePlay"); //Query -> Quem? Qual? Que tipo?

        startGameButton.RegisterCallback<ClickEvent>(StartGame);//cadastra no evento
    }
    void OnDisable()
    {
        startGameButton.UnregisterCallback<ClickEvent>(StartGame);//remove o cadastro
    }
    void StartGame(ClickEvent oClique)
    {
        //Debug.Log("Começa o Jogo aí");
        EventsMaster.OnStartGame?.Invoke("Level 1");
    }
}