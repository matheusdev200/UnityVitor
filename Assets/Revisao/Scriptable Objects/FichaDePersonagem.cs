using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Ficha de Personagem", menuName = "Fichas", order = 0)]
public class FichaDePersonagem : ScriptableObject
{
    //cadastrar o scriptable no menu
    //juntar as condicoes dele

    public string nomeDoPersonagem;
    public int nivelDoPersonagem;
    public int ataqueDoPersonagem;

    [Header("Prefab do Personagem")]
    public GameObject arteDoPersonagem;
    //QUALQUER tipo de variavel
}