using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class DialogueExample : MonoBehaviour
{
    public DialogueScriptable dialogue;
    TextMeshProUGUI textComponent;

    int lineIndex = 0;//contador de linha
    string currentLine;//cópia linha atual

    string[] lines;//uma cópia da lista que o scriptableObject tem

    IEnumerator Start()//:O
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        yield return new WaitUntil(() => textComponent != null);
        Debug.Log($"Texto pronto pra uso!");
        yield return new WaitForSeconds(0.5f);
        textComponent.text = string.Empty;
        yield return new WaitForSeconds(0.5f);
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//apertei espaço e o texto terminou de passar
        {
            //if (dialogo.gameObject.isActive())
            SkipLine();
        }
    }
    void StartDialogue()
    {
        lineIndex = 0;//reseta o contador de linha

        lines = dialogue.GetLines();//anota as falas que vai usar

        StartCoroutine(TypeDialogueRoutine());//comeca a coroutine de dialogo
    }
    IEnumerator TypeDialogueRoutine()
    {
        //currentLine = dialogue.GetDialogue();
        //char[] letters = currentLine.ToCharArray();

        //foreach (char letter in letters)
        //{
        //    textComponent.text += letter;
        //    yield return new WaitForSeconds(dialogue.textSpeed);
        //}char -> character
        foreach (char c in lines[lineIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(dialogue.textSpeed);
        }
    }
    void SkipLine()
    {
        if (textComponent.text != lines[lineIndex])
        {
            StopAllCoroutines();
            textComponent.text = lines[lineIndex];
        }
        else
        {
            if (lineIndex < lines.Length - 1)
            {
                lineIndex++;
                textComponent.text = string.Empty; //identico a escrever textComponent.text = ""; esvazia o texto
                StartCoroutine(TypeDialogueRoutine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
