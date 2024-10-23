using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarCameraman : MonoBehaviour
{
    public TiposDeCameras cameramanIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            GerenteDeCamera.instance.TrocaOCameraman(cameramanIndex);
        }
    }
}
