using UnityEngine;

//Create Asset Menu usa: Nome do Arquivo, local onde o arquivo vai aparecer, organizado como ("nome da pasta / nome do menu"), e depois a ordem
[CreateAssetMenu(fileName = "Novo Canhaum", menuName = "Scriptables/Canhoes", order = 1)]
public class CanonScriptable : ScriptableObject
{
    [SerializeField] GameObject canonArt;
    [SerializeField] string canonName;
    public GameObject LoadCanonArt() // carrega a arte do canhão
    {
        return canonArt;
    }
    public string LoadCanonName() // carrega o nome do canhão
    {
        return canonName;
    }
    //public void ChangeCanonArt(GameObject novaArte)
    //{
    //    canonArt = novaArte;
    //    //depois de atualizar
    //    //tem que marcar ele pra salvar -> pedir pra unity salvar os scriptables q eu mexi -> esperar ela terminar
    //    //-> salvar os scriptables q eu mexi e continuar a vida
    //}
}