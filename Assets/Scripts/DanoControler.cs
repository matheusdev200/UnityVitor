using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoControler : MonoBehaviour //script do projetil do canhao
{
    public float velocidade;
    public float tempoTiro;
    bool tiroAtivo = true;
    public int dano;
    float tempoLuz = 0.2f;
    public GameObject luz;
    public ParticleSystem explosao;
    void Update()
    {
        tempoTiro = tempoTiro - Time.deltaTime;
        tempoLuz = tempoLuz - Time.deltaTime;

        if (tempoLuz <= 0)
        {
            Destroy(luz);
        }
        transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);
        if (tempoTiro <= 0f)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    /*
        PointBulletToPlayer(Transform t){}
     */
    void OnTriggerEnter(Collider colliderAtingido)
    {
        //   SistemaVida vidaPlayer;

        if (colliderAtingido.TryGetComponent(out IDamage quemEuAcertei))
        {
            quemEuAcertei.TakeDamage((float)dano);
            tiroAtivo = false;
            StartCoroutine(DestroyComEfeito());
        }

        //if (acerteiVc.CompareTag("Player") && tiroAtivo)
        //{
        //    //if (acerteiVc.TryGetComponent(out SistemaVida v))
        //    //{
        //    //    vidaPlayer = v;
        //    //    tiroAtivo = false;
        //    //    vidaPlayer.TomaDano((float)dano); //-> Da dano no player;
        //    //}
        //    StartCoroutine(DestroyComEfeito());
        //}
    }
    IEnumerator DestroyComEfeito()
    {
        GetComponent<MeshRenderer>().enabled = false;
        velocidade = 0f;
        explosao.Play();

        yield return new WaitForSeconds(0.6f);
        Destroy(transform.parent.gameObject);
    }
}