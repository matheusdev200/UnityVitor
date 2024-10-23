using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    [Header("MOSTRAR GIZMO")]
    public Color corDoGizmo = Color.white;
    public bool ligarGizmo;

    [Header("CONFIGURACOES")]
    public float tempoDeAtualizacao;
    public float alcance;

    [Header("CAMADA")]
    public LayerMask camadaDoPlayer;
    protected Player p;//referencia para o player quando ele entrar no alcance
    protected bool active = true;
    [SerializeField] protected Vector3 ajuste = new Vector3(0f,0f,0f);
    Collider[] hits;
    protected IEnumerator CheckPlayer()
    {
        hits = Physics.OverlapSphere(transform.position + ajuste, alcance, camadaDoPlayer);//exemplo diferente
        if (hits.Length > 0)//verifica se algum collider tocou no tiro
        {
            hits[0].TryGetComponent(out Player playerReference);
            if (playerReference != null)
            {
                p = playerReference;   
            }
        }
        else
        {
            p = null;
        }
        yield return new WaitForSeconds(tempoDeAtualizacao);//tempo de atualiza��o da rotina de busca do alcance
        if (active)
        {
            StartCoroutine(CheckPlayer());
        } 
    }
    public virtual void PickUp()
    {
        
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
            Gizmos.DrawWireSphere(transform.position+ ajuste, alcance);
        }
    }
}