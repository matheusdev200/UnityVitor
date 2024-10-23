using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Input / Output

public static class SSystem
{
    //readonly impede algo que podia ser alterado de ser alterado
    //public static readonly string SAVE_FOLDER = Application.dataPath + "/SaveFiles/";//local dos arquivos de save
    public static string SAVE_FOLDER = Application.dataPath + "/SaveFiles/";                                                                                         //arquivos de save
    public static void Init()//faz o mesmo papel do void Awake
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void SetSaveFolderName(string folderPath)
    {
        SAVE_FOLDER = folderPath;
    }
    public static void Save(string saveString)
    {
        int saveNumber = 1;

        while (File.Exists(SAVE_FOLDER + "save_" + saveNumber + ".txt"))
        {
            saveNumber++;
        }
        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + ".txt", saveString);
    }
    public static string Load(int saveNumber)
    {
        if (File.Exists(SAVE_FOLDER + "/save_" + saveNumber + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save_" + saveNumber + ".txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}