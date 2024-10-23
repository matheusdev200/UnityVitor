using System.Collections;
using UnityEngine;

//script que controla o dano das coisas que o boss instancia (tiro, laser, onda de choque, etcs)
//e das colisoes dele (encostar, etcs)
public class BossDamage : MonoBehaviour
{
    public float damage;//dano da skill que o boss vai usar

    [Header("Configurações da Colisão")]
    public LayerMask playerLayer;
    public bool rectangleCollision;
    public float size; //raio caso seja esfera 
    public Vector3 rectSize; //tamanho caso seja retângulo
    public float collisionRefreshRate = 0.1f;

    [Header("Gizmos da Colisão")]
    public Color gizmosColor = Color.white;
    public bool showGizmos;

    public void PrepareBossSkillPrefab(float damage, bool col)
    {
        StartCoroutine(CheckForPlayerCollision());
    }
    IEnumerator CheckForPlayerCollision()
    {
        Collider[] cols;

        //testa a colisao contra o player
        if (rectangleCollision == true)//usando cubo
        {
            cols = Physics.OverlapBox(transform.position, rectSize, Quaternion.identity, playerLayer);
            if (cols[0].TryGetComponent(out IDamage quemEuAcertei))//se acertar o player
            {
                quemEuAcertei.TakeDamage(damage);//da o dano da skill nele
            }
        }
        else //usando esfera
        {
            //physics.overlapSphere
            cols = Physics.OverlapSphere(transform.position, size, playerLayer);
            if (cols[0].TryGetComponent(out IDamage quemEuAcertei))//se acertar o player
            {
                quemEuAcertei.TakeDamage(damage);//da o dano da skill nele
            }
        }

        yield return new WaitForSeconds(collisionRefreshRate);
        StartCoroutine(CheckForPlayerCollision());
    }
    void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmosColor;
            if (rectangleCollision == true)//usando cubo
            {
                Gizmos.DrawWireCube(transform.position, rectSize);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, size);
            }
        }
    }
}