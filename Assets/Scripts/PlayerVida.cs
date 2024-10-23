using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour, IDamage
{
    public int vida = 3;//vida que tá agora
    [HideInInspector] public int vidaMaxima = 3; //quanto de vida pode Ter
    //public bool playerTaVivo = true;

    void Start()
    {
        //EventsMaster.OnUpdatePlayerHealthbar();//grita pra quem quiser
        //ouvir / observar que tem que atualizar a vida
        EventsMaster.OnUpdatePlayerHealthbar?.Invoke();
    }

    //sobrecarga de Método
    public void TakeDamage(int dano)
    {
        vida = vida - dano;
        EventsMaster.OnUpdatePlayerHealthbar?.Invoke();
        if (vida <= 0)
        {
            //e morri :(
            //playerTaVivo = false;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float dano)
    {
        if (Player.instance.sistemaDeEnergia.HasEnergy() == true) //se tem energia
        {
            Player.instance.sistemaDeEnergia.BlockDamageWithEnergy(dano);
        }
        else //se acabou a energia
        {
            //TomaDano((int)dano); ô/i
            vida = vida - (int)dano;
            EventsMaster.OnUpdatePlayerHealthbar?.Invoke();
            if (vida <= 0)
            {
                //e morri :(
                //playerTaVivo = false;
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage()
    {

    }
    public void HealDamage() { }
}
