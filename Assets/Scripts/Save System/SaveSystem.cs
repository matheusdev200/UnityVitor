using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//Input / Output

public class SaveSystem
{
    public static string SaveFolderPath = Application.dataPath + "/Resources/Saves/";
    public static int selectedSave = 1;
    //manualmente colocar a classe start nele
    public static void Init()
    {
        if (!Directory.Exists(SaveFolderPath))
        {
            Directory.CreateDirectory(SaveFolderPath);
        }
    }
    public static void Save(string jsonFileToSave)
    {
        selectedSave = SaveManager.instance.selectedSave;
        //verificar se o arquivo ja existe
        File.WriteAllText(SaveFolderPath + "save_" + selectedSave + ".txt", jsonFileToSave);
    }
    public static SaveSlot Load()
    {
        selectedSave = SaveManager.instance.selectedSave;
        //verificar se o arquivo existe
        string saveFile = $"{SaveFolderPath}save_{selectedSave}.txt";
        string fileContent = File.ReadAllText($"{SaveFolderPath}save_{selectedSave}.txt");
        if (File.Exists(saveFile))
        {
            SaveSlot slot = JsonUtility.FromJson<SaveSlot>(fileContent);
            //Debug.Log($"save number: {slot.slot} was opened with {slot.playerSavedPosition} position and {slot.lastSavedStation} station");
            return slot;
        }
        else
        {
            //Debug.Log("Deu ruim moço");
            return null;
        }
    }
}