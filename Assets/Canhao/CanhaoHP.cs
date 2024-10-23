using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// : heran�a , interfaces
public class CanhaoHP : MonoBehaviour, IDamage
{
    //adicionar aqui o scriptable com os valores do canhao

    int vida = 3;//vida que t� agora
    public int vidaMaxima = 3;

    Vector2 playerPosition;

    // public void TakeDamage()//causa dano
    // {

    // }
    void OnEnable()
    {
        EventsMaster.OnGetLoadGame += ReadLoad;
    }
    void OnDisable()
    {
        EventsMaster.OnGetLoadGame -= ReadLoad;
    }
    void Start()
    {
        //EventsMaster.OnGetLoadGame?.Invoke();
    }

    void ReadLoad()
    {
        playerPosition = SaveManager.instance.currentLoadedGame.playerSavedPosition;
        transform.position = playerPosition;
    }
    public void TakeDamage(float d)//causa dano
    {

    }
    public void TakeDamage(int d)//causa dano
    {
        vida -= d;
        Die();
    }
    public void HealDamage()//cura dano
    {

    }
    public void Die() //vida chega em zero, e foi de arrasta
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

}