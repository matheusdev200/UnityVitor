using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede : MonoBehaviour
{
    void OnTriggerEnter(Collider quemEsbarrouEmMim)//parede
    {
        //if (quemEsbarrouEmMim.gameObject.layer == 3)
        //{
        //    PlayerMovement.parede = true; //a variavel estática
        //    if (quemEsbarrouEmMim.transform.position.x > transform.position.x)
        //    {
        //        PlayerMovement.paredeNaDireita = true;
        //    }
        //}
        if (quemEsbarrouEmMim.TryGetComponent(out PlayerMovement player))
        {
            player.parede = true; //a variavel estática
            if (player.transform.position.x > transform.position.x)
            {
                player.paredeNaDireita = true;
            }
            if (player.transform.position.x < transform.position.x)
            {
                player.paredeNaDireita = false;
            }
            PlayerPrefs.DeleteKey("");
        }
    }
}
