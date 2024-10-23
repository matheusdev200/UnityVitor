using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public int selectedSave = 1;
    //public Vector2 examplePlayerPosition;
    //public int exampleSavedStation;

    public SaveSlot currentLoadedGame;
    void Awake()
    {
        if (instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SaveSystem.Init(); //da start no sistema de save
    }
    IEnumerator Start()
    {
        EventsMaster.OnSaveGame += SaveCurrentSlot; // registra no evento
        EventsMaster.OnLoadGame += LoadGame; // registra no evento

        EventsMaster.OnLoadGame?.Invoke(SaveSystem.Load());

        yield return new WaitUntil(() => currentLoadedGame != null);
        EventsMaster.OnGetLoadGame?.Invoke();
    }
    void OnDisable()
    {
        EventsMaster.OnSaveGame -= SaveCurrentSlot; // remove o registro do evento
        EventsMaster.OnLoadGame -= LoadGame; // remove o registro do evento
    }
    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.S))
    //    //{
    //    //    EventsMaster.OnSaveGame?.Invoke(examplePlayerPosition, exampleSavedStation);
    //    //}
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        //EventsMaster.OnLoadGame?.Invoke(SaveSystem.Load());
    //        //EventsMaster.OnGetLoadGame?.Invoke();
    //    }
    //}
    void SaveCurrentSlot(Vector2 terminalPosition, int whichSaveStation)
    {
        //verificar qual estação esta sendo salva

        // variavel do tipo da classe => new Class {parametro1, parametro2, etcs};
        SaveSlot slotToSave = new SaveSlot
        {
            //aqui dentro preenche na hora de salvar cada informacao de SaveSlot
            slot = selectedSave,
            playerSavedPosition = terminalPosition,
            currentPanelCode = whichSaveStation//vem do gerente de terminais
        };

        //salva em um objeto convertido para JSON -> vira um .txt
        string jsonToSave = JsonUtility.ToJson(slotToSave);

        SaveSystem.Save(jsonToSave);//pede pro sistema de save guardar essa info na pasta de save
    }
    void LoadGame(SaveSlot slot)
    {
        currentLoadedGame = slot;
        Debug.Log("Game loaded");
        //manda abrir a cena
        //no start de alguma coisa da cena chama o evento ->EventsMaster.OnGetLoadGame?.Invoke();
    }
    void ReadLoad()//Gerente de Verde, player, tutorial 
    {
        //painel de controle -> vermelho
        //estação de save -> verde

        //metodo que le os objetos do save que geram alguma informacao

        //Verde 2                            Verde 3                       Verde 4
        //             Verm2,Verm3,Verm4                    Verm5 Verm6       
    }
}
public class SaveSlot //arquivo da campanha -> arquivo que vai virar um JSON
{
    //tudo que vier aqui, e informacao que vai ser salva
    //public string playerName;
    //qual campanha
    public int slot;
    //coisas do player
    public Vector2 playerSavedPosition;
    //coisas do mundo
    public int currentPanelCode;
}

/*
 //gaveta
public class SaveSlot { } //arquivo com as informações do que vai ser salvo
//funcionario que mexe no armario e na cena
public class SaveManager : MonoBehaviour { } //Gerente de Save
//armario onde a gaveta vai ser guardada
public static class SaveSystem { } //script estatico que controla o arquivo de save no sistema
 */