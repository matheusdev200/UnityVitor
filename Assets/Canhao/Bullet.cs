using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour //PREFAB DO TIRO
{
    public int damage; //dano do player
    float bulletSpeed = 2f;
    float bulletTimer = 3f;
    Vector3 flyDirection;

    [Header("CONFIGURA��ES DE COLIS�O")]
    public float bulletRefreshRate;
    public float damageRange;
    //public LayerMask damageableLayers;
    //RaycastHit hit;
    Collider[] hits;

    [Header("MOSTRAR GIZMO")]
    public Color corDoGizmo = Color.white;
    public bool ligarGizmo;

    void OnEnable()
    {
        StartCoroutine(BulletTimerRoutine());
        StartCoroutine(BulletCollisionRoutine());
    }
    void Update()
    {
        transform.Translate(flyDirection * bulletSpeed * Time.deltaTime);
    }
    IEnumerator BulletCollisionRoutine()
    {
        //if (Physics.SphereCast(transform.position, damageRange, transform.forward, out hit))//verifica se algum collider tocou no tiro
        //{
        //    hit.collider.TryGetComponent(out IDamage c);
        //    if (c != null)
        //    {
        //        c.TakeDamage(damage);
        //    }

        //    PoolingManager.ReturnObjectToPool(gameObject);
        //}
        hits = Physics.OverlapSphere(transform.position, damageRange);//exemplo diferente
        if (hits.Length > 0)//verifica se algum collider tocou no tiro
        {
            hits[0].TryGetComponent(out IDamage inimigo);
            if (inimigo != null)
            {
                inimigo.TakeDamage(damage); //control K+C
            }

            PoolingManager.ReturnObjectToPool(gameObject);
        }
        yield return new WaitForSeconds(bulletRefreshRate);
        StartCoroutine(BulletCollisionRoutine());
    }
    IEnumerator BulletTimerRoutine()
    {
        float timer = 0f;

        while (timer < bulletTimer) //jeito alternativo de fazer um timer
        {
            timer += Time.deltaTime;
            yield return null;
        }
        PoolingManager.ReturnObjectToPool(gameObject);
    }
    public void LoadBullet(Vector3 direction, float speed, float timer)
    {
        bulletSpeed = speed;
        bulletTimer = timer;
        flyDirection = direction;//gambiarra pra virar o objeto pra algum lado
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
            Gizmos.DrawWireSphere(transform.position, damageRange);
        }
    }
}