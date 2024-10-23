using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposDeCameras 
{
    CameraDePerto, CameraMedia, CameraDeLonge
}

//singleton
public class GerenteDeCamera : MonoBehaviour
{
    public static GerenteDeCamera instance;
    public List<GameObject> cameramans = new List<GameObject>();//lista é "mais permanente" do que um Array
    void Awake()
    {
        if (instance != this) //se tiver outro desse em outro lugar, deleta ele 
        {
            Destroy(instance);
        }
        instance = this;
    }

    public void TrocaOCameraman(TiposDeCameras cameramanIndex)  //escolhe um cameraman da lista e ativa ele, desativa os outros em seguida
    {
        for (int i = 0; i < cameramans.Count; i++)
        {
            if (i == (int)cameramanIndex)//(tipo) -> casting -> conversão de medidas explícita
                //(quilos)pesoEmGramas => Kg 
            {
                cameramans[i].SetActive(true);
            }
            else
            {
                cameramans[i].SetActive(false);
            }
        }
    }
}