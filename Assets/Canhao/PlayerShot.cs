using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour, IPausable //ARMA DO PLAYER
{
    [Header("PREFAB E CONFIGURAÇÕES")]
    public GameObject bullet; //prefab do tiro
    //public GameObject chargedBullet; //prefab do tiro
    public float bulletSpeed;
    public float bulletDuration;
    //public float shotCooldown;
    [SerializeField] Transform[] gunPoints;
    int currentGun = 0;

    [Header("EFEITOS")]
    public GameObject spawnerEfeito; //efeito pra melhorar o surgimento do tiro (Antecipação)
    public AudioClip somDotiro;

    AudioSource caixaDeSom;

    void Update()
    {
        if (Pause.gameIsPaused == true) return;

        if (Input.GetMouseButtonDown(1))//TIRO NORMAL
        {
            ShootSomething(bullet);
        }
        //if (Input.GetMouseButtonUp(1))//TIRO CARREGADO
        //{
        //    ShootSomething(chargedBullet);
        //}
    }
    void ShootSomething(GameObject projectilePrefab)
    {
        Bullet currentShot = PoolingManager.SpawnObject(projectilePrefab, gunPoints[currentGun].position, Quaternion.identity).GetComponent<Bullet>();
        currentShot.LoadBullet(transform.forward, bulletSpeed, bulletDuration);
        if (currentGun + 1 > 1)
        {
            currentGun = 0;
        }
        else
        {
            currentGun++;
        }
    }
    public bool IsPaused()
    {
        return Pause.gameIsPaused;
    }
}
/*
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
}
 */