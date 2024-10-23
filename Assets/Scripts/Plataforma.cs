using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    bool moveVertical;
    public Transform PontoA;
    public Transform PontoB;
    public float velocMovimento;
    void Start()
    {

    }
    void Update()
    {
        PlataformaVertical();
    }
    void PlataformaVertical()
    {
        if (transform.position.y < PontoA.position.y)
        {
            moveVertical = true;
        }
        if (transform.position.y > PontoB.position.y)
        {
            moveVertical = false;
        }
        if (moveVertical)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocMovimento * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocMovimento * Time.deltaTime);
        }
    }
}