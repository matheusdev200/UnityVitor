using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanastraoMira : MonoBehaviour
{
    Animator miraAnimator;
    public bool vejoOPlayer = true;

    [Range(0.1f, 1.5f)] public float tempoDeEspera;
    void Awake()
    {
        miraAnimator = GetComponent<Animator>();
    }
    public void Mirei()
    {
        StartCoroutine(AimControlRoutine());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            miraAnimator.SetBool("Mirando", true);
            miraAnimator.SetFloat("Multiplicador de Velocidade", 1);
        }
    }
    IEnumerator AimControlRoutine()
    {
        Debug.Log("Terminei a minha mira canastrona :)");
        miraAnimator.SetFloat("Multiplicador de Velocidade", 0);
        yield return new WaitUntil(() => vejoOPlayer == false);

        miraAnimator.SetFloat("Multiplicador de Velocidade", -1);

        yield return new WaitForSeconds(tempoDeEspera);//achar um jeito de colocar o tempo do clipe aqui se necessario
        //?
        miraAnimator.SetBool("Mirando", false);
    }
}