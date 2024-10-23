using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Ferramenta", menuName = "Scriptables/Ferramentas", order = 0)]
//Personagem : ScriptableObject
public class ToolScriptable : ScriptableObject
{
    //combat properties
    [Header("Propriedades na Loja")] 
    public string toolName;
    public float toolCost;
    public int inventorySize;

    [Header("Tipo de Ferramenta")]
    public float useRange;
    public bool breakable = true;
    public int durability = 5;
    public GameObject effectPrefab;
    [Tooltip("o prefab da arte da ferramenta")] public GameObject toolPrefab;
}