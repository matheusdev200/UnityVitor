using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour //isso vai no player
{
    PlayerInput playerInput;
    InputAction activateEnergyAction;//tipo de vari�vel que guarda uma a��o do mapa de a��es
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        activateEnergyAction = playerInput.actions.FindAction("Gasta Carga");
    }
    void Update()
    {
        if (activateEnergyAction.WasPressedThisFrame())//apertou o bot�o de Gastar Carga
        {
            Player.instance.tentouInteragir = true;
        }
        else
        {
            Player.instance.tentouInteragir = false;
        }
    }
}