using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : ItemCollectable
{
    Rigidbody rb;
    //scriptable item
    void Start()
    {
        StartCoroutine(CheckPlayer());
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        #region detritos
        if (Pause.gameIsPaused && rb.isKinematic == false)
        {
            rb.isKinematic = true;
            return;
        }
        if (!Pause.gameIsPaused && rb.isKinematic == true)
        {
            rb.isKinematic = false;
        }
        //verifica a altura do lixo e se passar de x valor destroi ele 
        #endregion detritos

        if (p != null && active == true)
        {
            PickUp();
        }
    }
    public override void PickUp()
    {

        active = false;
        //faz a conta de quantos por cento de energia vai encher
        //float playerMaxEnergy = p.sistemaDeEnergia.maxEnergy;
        //float energy = 0.3f * playerMaxEnergy;
        //manda recarregar essa quantidade
        //p.sistemaDeEnergia.RechargeEnergy(energy);

        //jeito rapido e com os calculos dentro do parenteses
        p.sistemaDeEnergia.RechargeEnergy(0.3f * p.sistemaDeEnergia.maxEnergy); //:D

        Destroy(gameObject);
        /*
        resolve o que o item faz
        */
    }
}