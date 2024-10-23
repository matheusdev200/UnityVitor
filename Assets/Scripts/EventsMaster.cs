using System;
using UnityEngine; //OBS: Baixo Nível -> mais próximo da linguagem do hardware (de máquina / binário)
                   //Alto Nível -> mais próxima do usuário (programador)

public class EventsMaster //NAO responde.
{
    //Observer / Observador                         //Se algo (o Evento) acontecer, responde com a Acao
    public static Action OnUpdatePlayerHealthbar; //Event Action -> Evento de Acao

    public static Action OnKillPlayer;

    public static Action<float> OnTrySpendEnergy;

    public static Action<string> OnStartGame;
    public static Action OnChangeFPS;//evento que mostra ou nao o fps na tela

    //Eventos de Save
    public static Action<Vector2, int> OnSaveGame; // salva
    public static Action<SaveSlot> OnLoadGame;//carrega e le o que foi salvo
    public static Action OnGetLoadGame; //avisa que tem algo carregado e lido que pode usar

    //EVENTOS DE BOSS
    public static Action OnStartBossFight;
    public static Action OnFinishBossFight;
}