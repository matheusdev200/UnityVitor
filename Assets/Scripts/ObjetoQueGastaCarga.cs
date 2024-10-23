using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoQueGastaCarga : MonoBehaviour
{
    public float quantaCarga;

    public bool active;
    public bool posicaoOkay;

    PlayerEnergy sistemaDeEnergiaDoPlayer;
    void OnTriggerEnter(Collider other)//player chegou aqui perto
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerEnergy p))
            {
                sistemaDeEnergiaDoPlayer = p; //anota a referência do sistema de energia do player
                posicaoOkay = true;
            }
        }
    }
    void OnTriggerStay(Collider other)//player continua aqui perto
    {
        if (other.CompareTag("Player"))
        {
            if (posicaoOkay == true && Player.instance.tentouInteragir == true && active == false)//faz a ação quando o
                                                                                                  //player tentar interagir
            {
                active = sistemaDeEnergiaDoPlayer.SpendEnergy(quantaCarga);
            }
        }
    }
    void OnTriggerExit(Collider other)//player foi-se embora
    {
        if (other.CompareTag("Player"))
        {
            sistemaDeEnergiaDoPlayer = null;
            posicaoOkay = false;
        }
    }
}
