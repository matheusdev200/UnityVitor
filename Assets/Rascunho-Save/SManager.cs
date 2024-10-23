using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SManager : MonoBehaviour
{
    public int selectedSave = 1; //variavel pra controlar qual save esta sendo usado
    public static SManager instance;
    public string folderPath;
    void Awake()
    {
        SSystem.SetSaveFolderName(folderPath);
        SSystem.Init();

        if (instance != this)
            Destroy(instance);

        instance = this;
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    public void SaveCurrentSlot(Vector2 currentPlayerPosition, int lastTerminalUsed)
    {
        //convertendo o objeto pra JSON
        SSlot currentObjectToSave = new SSlot //c# raiz de inicializacao de Classe
        {
            //listando os valores que as variaveis da instancia v?o ter
            slot = selectedSave,
            playerSavedPosition = currentPlayerPosition,
            lastSavedTerminal = lastTerminalUsed
        };
        // variavel do tipo da classe => new Class {parametro1, parametro2, etcs};
        string jsonContent = JsonUtility.ToJson(currentObjectToSave);
        //salva em um objeto convertido para JSON -> vira um .txt
        SSystem.Save(jsonContent);//salva o arquivo na pasta :)
    }
}
public class SSlot
{
    public int slot;
    public Vector2 playerSavedPosition;
    public int lastSavedTerminal;
}