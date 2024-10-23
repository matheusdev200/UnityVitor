using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour //isso vai no player
{
    PlayerInput playerInput;
    InputAction activateEnergyAction;//tipo de variável que guarda uma ação do mapa de ações
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
        if (activateEnergyAction.WasPressedThisFrame())//apertou o botão de Gastar Carga
        {
            Player.instance.tentouInteragir = true;
        }
        else
        {
            Player.instance.tentouInteragir = false;
        }
    }
}