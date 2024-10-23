using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Checkpoint : MonoBehaviour
{
    public Color32 corDoEfeito;
    MeshRenderer materialDoCheckpoint;

    bool active = true;
    [SerializeField] int danoDoTiro = 1;
    void Awake()
    {
        materialDoCheckpoint = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            materialDoCheckpoint.material.color = corDoEfeito;
        }
    }
    public void SalvaOCheckpoint()
    {
        PlayerPrefs.SetInt("Checkpoint", 1);
        Debug.Log($"salvei em {PlayerPrefs.GetInt("Checkpoint")} :)");
    }
    void EfeitoDoCheckpoint()
    {
        materialDoCheckpoint.material.color = corDoEfeito;
    }
    void OnTriggerEnter(Collider other)
    {

        //if (other.CompareTag("Player"))
        //{
        //    SalvaOCheckpoint();
        //    EfeitoDoCheckpoint();
        //}
        Player oPlayer;//cria uma variável temporária pra referência do player
        if (other.CompareTag("Player") && active)
        {
            if (other.TryGetComponent(out Player p))
            {
                oPlayer = p;
                //active = false;
                //oPlayer.sistemaDeVida.TomaDano((float)danoDoTiro); //-> Da dano no player;
                Destroy(transform.parent);
            }
        }
    }

}