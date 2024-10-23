using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTiroInimigo : MonoBehaviour
{
    [Header("MOSTRAR GIZMO")]
    public Color corDoGizmo = Color.white;
    public bool ligarGizmo;

    [Header("PREFAB E CONFIGURAÇÕES")]
    public GameObject tiro; //prefab do tiro
    public float tempoSpawner;
    public float alcance;
    [SerializeField] Transform tiroSpawner;

    [Header("EFEITOS")]
    public GameObject spawnerEfeito; //efeito pra melhorar o surgimento do tiro (Antecipação)
    public AudioClip somDotiro;

    [Header("CAMADA")]
    public LayerMask camadaDoPlayer;

    //referências
    AudioSource caixaDeSom;
    bool playerNoAlcance;

    //public Transform comecaAtirar;
    //public Transform player;
    void Awake()
    {
        caixaDeSom = GetComponent<AudioSource>();
        caixaDeSom.clip = somDotiro;
    }
    void Start()
    {
        StartCoroutine(LaserRoutine());
        //InvokeRepeating(nameof(Spawn), tempoSpawner, tempoSpawner);
    }
    //void Spawn()
    //{
    //    Instantiate(tiro, tiroSpawner.position, Quaternion.identity);
    //}

    IEnumerator LaserRoutine()
    {
        //verifica se o player ta no alcance
        if (Physics.OverlapSphere(transform.position, alcance, camadaDoPlayer).Length != 0)
        {
            playerNoAlcance = true;
        }
        else
        {
            playerNoAlcance = false;
        }
        if (playerNoAlcance)
        {
            yield return StartCoroutine(ChargeLaser());
        }
        yield return new WaitForSeconds(0.1f);//tempo de atualização da rotina de busca do alcance
        StartCoroutine(LaserRoutine());
    }
    IEnumerator ChargeLaser()
    {
        spawnerEfeito.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(tempoSpawner);
        caixaDeSom.Play();
        Instantiate(tiro, tiroSpawner.position, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        if (!ligarGizmo)
        {
            return;
        }
        else
        {
            Gizmos.color = corDoGizmo;
            Gizmos.DrawWireSphere(transform.position, alcance);
        }
    }
}