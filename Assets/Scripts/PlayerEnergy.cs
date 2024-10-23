using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy; // quanta energia o player pode ter
    float currentEnergy; // quanta energia o player tem agora
    public float energySpeed = 1f; //0f -> não perde energia, 1f -> perde 1 unidade de energia por segundo
    public bool active;

    [SerializeField] Image barraDeEnergia;

    float larguraInicialDaBarraDeEnergia;
    float porcentagemDeEnergia; //anota quanto vale os pontos de vida atuais em porcentagem

    void OnEnable()
    {
        //EventsMaster.OnTrySpendEnergy += SpendEnergy;
    }
    void OnDisable()
    {
        //EventsMaster.OnTrySpendEnergy -= SpendEnergy;
    }
    //void Awake()
    //{
    //    larguraInicialDaBarraDeEnergia = barraDeEnergia.rectTransform.sizeDelta.x;
    //}
    void Start()
    {
        StartCoroutine(DischargeEnergyRoutine());//ativa a coroutine ao carregar
        currentEnergy = maxEnergy;
    }
    IEnumerator DischargeEnergyRoutine() //controla o gasto passivo de energia
    {
        if (currentEnergy > 0f)
        {
            currentEnergy -= Time.deltaTime * energySpeed;

            //calcular quanta energia é 100%
            porcentagemDeEnergia = currentEnergy / maxEnergy;
            //valor atual / valor total
            float novaLarguraDaBarra = porcentagemDeEnergia * larguraInicialDaBarraDeEnergia;
            // ajeitar a escala de largura da barra de vida em relação à porcentagem atual
            barraDeEnergia.rectTransform.sizeDelta = new Vector2(novaLarguraDaBarra,
                barraDeEnergia.rectTransform.sizeDelta.y);
        }
        else
        {
            active = false;
        }
        yield return new WaitForSeconds(Time.deltaTime * energySpeed);//aguarda um segundo

        if (active)
        {
            StartCoroutine(DischargeEnergyRoutine());
        }
    }
    public bool HasEnergy()
    {
        bool temEnergia = false;

        if (currentEnergy != 0f) //se tem energia
        {
            temEnergia = true; //então, tem energia
        }
        else //se não tem energia
        {
            temEnergia = false; //então, não tem energia
        }
        return temEnergia;//responde quem perguntou, se tem ou se não tem energia :)
    }
    public void BlockDamageWithEnergy(float damage)
    {
        float calcEnergy = currentEnergy - damage; //energia calculada
        if (calcEnergy >= 0f) //pode gastar energia;
        {
            currentEnergy = calcEnergy;
        }
        else
        {
            currentEnergy = 0f;
            active = false;
        }
    }
    public void RechargeEnergy(float charge)
    {
        float calcEnergy = currentEnergy + charge; //energia calculada
        if (calcEnergy > maxEnergy) //se a carga é maior do que a bateria aguenta,
                                    //carrega só o quanto a bateria aguenta
        {
            currentEnergy = maxEnergy;
        }
        else //se cabe mais energia, carrega
        {
            currentEnergy = calcEnergy;
        }
        if (!active)//se tinha acabado a energia, liga a rotina de descarregar de novo
        {
            active = true;
            StartCoroutine(DischargeEnergyRoutine());
        }
    }
    public bool SpendEnergy(float energy)
    {
        float calcEnergy = currentEnergy - energy; //energia calculada
        if (calcEnergy >= 0f) //pode gastar energia;
        {
            currentEnergy = calcEnergy;
            //Debug.Log($"Gastou {energy} de energia");
            return true;
        }
        else
        {
            //Debug.Log($"Não pode gastar energia pq tá caro :(");
            return false;
        }
    }
}