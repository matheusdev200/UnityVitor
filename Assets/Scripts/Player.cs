using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int dano = 1;

    public bool tentouInteragir;//flag que ativa quando o player tentar gastar energia

    [HideInInspector] public PlayerVida sistemaDeVida; //a variável do sistema no PLAYER
                                                       //recebe o SCRIPT de PlayerVida
    [HideInInspector] public PlayerEnergy sistemaDeEnergia;
    [HideInInspector] public PlayerMovement sistemaDeMovimento;

    void Awake()
    {
        instance = this;

        if (instance != this)
        {
            Destroy(instance);
        }

        sistemaDeVida = GetComponent<PlayerVida>();
        sistemaDeMovimento = GetComponent<PlayerMovement>();
        sistemaDeEnergia = GetComponent<PlayerEnergy>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            EventsMaster.OnTrySpendEnergy?.Invoke(25f);
        }
    }
}