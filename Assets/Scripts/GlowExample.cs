using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowExample : MonoBehaviour
{
    public MeshRenderer myRenderer; //tela
    [ColorUsage(showAlpha: true, hdr: true)] public Color glowColor;//cor do brilho

    void LightScreenColor()//acende a tela
    {
        myRenderer.material.SetColor("Glow_Color", glowColor);
    }
    IEnumerator ApagaAcende()
    {
        myRenderer.material.SetColor("Glow_Color", Color.black /*atalho pra "preto"*/); // apaga
        yield return new WaitForSeconds(2f); //espera
        myRenderer.material.SetColor("Glow_Color", glowColor); // acende
    }
}
