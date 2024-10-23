using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//gerenciamento de cenas

public class SceneMaster : MonoBehaviour
{
    public SceneMaster instance;//precisa ser static pra todos os outros scripts conseguirem chamar o instance dele
    public bool startScene = false;
    void OnEnable()
    {
        EventsMaster.OnStartGame += ChangeScene;
    }
    void OnDisable()
    {
        EventsMaster.OnStartGame -= ChangeScene;
    }

    void Awake()
    {
        if (instance != this)//verifica se tem mais que um instance na cena
        {
            Destroy(instance);//se tiver deleta ele
        }
        instance = this;//assinala ele próprio como instance, e se sobra só ele, ele fica
    }
    void Start()
    {
        if (startScene)
        {
            //ChangeScene("Main Menu");
            //ChangeScene(2);
            StartCoroutine(ChangeSceneRoutine(0.5f, 2));//aguarda 2 segundos e abre a cena 2
        }
    }
    public IEnumerator ChangeSceneRoutine(int sceneNumber)
    {
        ChangeScene(sceneNumber);
        yield return null;
    }
    public IEnumerator ChangeSceneRoutine(float waitTime, int sceneNumber)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeScene(sceneNumber);
    }
    public void ChangeScene(string sceneName)//chama a cena pelo nome
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeScene(int sceneNumber)//chama a cena pelo RG
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void QuitGame()
    {
        //Debug.Log("Tentou fechar o jogo");
        Application.Quit();
    }
}