using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    [Header("Gizmos Properties")]
    public Color gizmoColor = Color.white;
    public bool showGizmo = true;

    [Header("Collision Properties")]
    public float updateFrequency = 0.1f;
    public float collisionRadius = 2f;
    public LayerMask playerLayer;

    public MeshRenderer mr;
    [ColorUsageAttribute(showAlpha: true, hdr: true)] public Color32 glowColor;

    void Start()
    {
        StartCoroutine(CheckForPlayerTriggerRoutine());
    }
    IEnumerator CheckForPlayerTriggerRoutine() //verifica se o player está no nosso trigger
    {
        if (Physics.OverlapSphere(transform.position, collisionRadius, playerLayer).Length > 0)//esse faz OnTriggerStay
        {
            Debug.Log("Player encostou aqui", gameObject);
        }

        /*if (Physics.SphereCast())//esse faz OnTriggerEnter
        //{

        //}*/

        yield return new WaitForSeconds(updateFrequency);//de quanto em quanto tempo testa se o player está no trigger
        StartCoroutine(CheckForPlayerTriggerRoutine());
    }
    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            //desenha a colisão na tela
            Gizmos.DrawWireSphere(transform.position, collisionRadius);
        }
    }
    void ChangeColor()
    {
        mr.material.SetColor("Glow_Color", glowColor);
    }
}
