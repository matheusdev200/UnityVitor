using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// classe que usa os atributos -> atributos <- scriptable object que passa os atributos
public class ToolBehaviour : MonoBehaviour
{
    [SerializeField] ToolScriptable[] myToolScriptable;
    [SerializeField] Transform effectLocation;
    //public SceneMaster gerenteDeCena;

    //propriedades da ferramenta atual
    string myToolName;
    float myUseRange;
    bool myBreakable;
    int myDurability;
    GameObject toolArt;

    void Start()
    {
        //LoadToolAttributes(myToolScriptable);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Instantiate(myToolScriptable.effectPrefab, effectLocation.position, Quaternion.identity);//instancia o efeito especial de usar a ferramenta
            PoolingManager.SpawnObject(myToolScriptable[0].effectPrefab, effectLocation.position, Quaternion.identity);
            myDurability -= 1;
            Debug.Log($"Usou a ferramenta {myToolName} e gastou 1 de durabilidade");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadToolAttributes(myToolScriptable[1]);
            Debug.Log($"trocou para a Picareta");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadToolAttributes(myToolScriptable[0]);
            Debug.Log($"trocou para a Pá");
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    gerenteDeCena.startScene = false;
        //}
    }
    void LoadToolAttributes(ToolScriptable newTool)
    {
        if (toolArt != null)
        {
            Destroy(toolArt);
        }
        //variável do objeto na cena (variáveis locais) -> referencia no scriptable object 
        myToolName = newTool.toolName;
        myUseRange = newTool.useRange;
        myBreakable = newTool.breakable;
        myDurability = newTool.durability;
        GameObject tempArt = Instantiate(newTool.toolPrefab, transform);//instancia a arte da ferramenta no objeto
        toolArt = tempArt;
        //e pega uma referência de qual é esse objeto caso precisemos mexer com ele posteriormente
    }
}
