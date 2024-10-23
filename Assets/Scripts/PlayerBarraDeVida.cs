using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Gerente da barra de vida
public class PlayerBarraDeVida : MonoBehaviour
{
    TextMeshProUGUI textoDaVida;
    [SerializeField] Image barraDeVida;
    float larguraInicialDaBarraDeVida;
    float larguraDaBarraDeVida; //anota quanto vale o tamanho da barra de vida cheia
    float porcentagemDaVida; //anota quanto vale os pontos de vida atuais em porcentagem
    public float timeToUpdateBar = 0.8f;

    void OnEnable()
    {
        //chama o evento -> acrescenta na lista de Ações que respondem ele, o método que precisa
        EventsMaster.OnUpdatePlayerHealthbar += AtualizarBarraDeVida;
    }
    void OnDisable()
    {
        EventsMaster.OnUpdatePlayerHealthbar -= AtualizarBarraDeVida;
    }
    void Awake()
    {
        textoDaVida = GetComponentInChildren<TextMeshProUGUI>();
        larguraInicialDaBarraDeVida = barraDeVida.rectTransform.sizeDelta.x;
    }
    void Start()
    {
        larguraDaBarraDeVida = larguraInicialDaBarraDeVida;
    }
    //resolve com referência
    public void AtualizarBarraDeVida()
    {
        //calcular quanta vida é 100%
        porcentagemDaVida = (float)Player.instance.sistemaDeVida.vida / (float)Player.instance.sistemaDeVida.vidaMaxima;
        //valor atual / valor total
        float novaLarguraDaBarra = porcentagemDaVida * larguraInicialDaBarraDeVida;
        // ajeitar a escala de largura da barra de vida em relação à porcentagem atual
        StartCoroutine(UpdateBarRoutine(novaLarguraDaBarra));
        string textoDaPorcentagem = (porcentagemDaVida * 100f).ToString("N0");
        textoDaVida.text = $"{Player.instance.sistemaDeVida.vida} HP ({textoDaPorcentagem}%)";
    }
    IEnumerator UpdateBarRoutine(float larguraAtualizada)
    {
        bool active = true;
        float timer = 0f;
        float l = 0f;

        if (active)
        {
            while (timer < timeToUpdateBar)
            {
                timer += Time.deltaTime;
                //Ajusta larguraDaBarraDeVida na "direção" da larguraAtualizada, conforme timer / timeToUpdateBar
                //                                                          (que é um tempo) passa
                l = Mathf.Lerp(larguraDaBarraDeVida, larguraAtualizada, timer / timeToUpdateBar);

                //sizeDelta não pode alterar altura ou largura em separado, então passamos um vector2 novo
                //alterando a largura e não mexendo na altura (passando o valor que já tava no sizeDelta em y)
                barraDeVida.rectTransform.sizeDelta = new Vector2(l, barraDeVida.rectTransform.sizeDelta.y);

                yield return null;
            }
            larguraDaBarraDeVida = larguraAtualizada;
            yield return null;
        }
    }
}