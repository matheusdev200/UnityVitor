using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiTiro : MonoBehaviour
{
    public Transform sumiu;
    void Update()
    {
        if (transform.position.x <= sumiu.position.x)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
