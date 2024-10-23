using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [SerializeField] Collider myTrigger;
    [SerializeField] ParticleSystem myParticle;
    MeshRenderer myrenderer;
    void Start()
    {
        myTrigger = GetComponent<Collider>();
        myParticle = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        if (myParticle.isStopped)
        {
            myTrigger.enabled = false;
        }
        else
        {
            myTrigger.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //método de dano
        }

        //myrenderer.material.
    }
}
