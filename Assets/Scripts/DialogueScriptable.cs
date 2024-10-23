using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptables/Conversinha", order = 2)]
public class DialogueScriptable : ScriptableObject
{
    public float textSpeed; //em segundos

    [SerializeField] [TextArea] string[] dialogueLines;
    [SerializeField] TMP_FontAsset font; //estilo da fonte criado com o TMPRO Font Asset Creator
    //color, font size, etcs

    public string GetLines(int textIndex)
    {
        return dialogueLines[textIndex];
    }
    public string[] GetLines()
    {
        return dialogueLines;
    }
}