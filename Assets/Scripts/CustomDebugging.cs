using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDebugging : MonoBehaviour
{
    public int variavel = 2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CustomDebugTest();
        }
    }
    void CustomDebugTest()
    {
        //Debug.Log("o <b>valor</b> da minha variavel é:" + variavel + "e <color=red>vermelho</color>");
        Debug.Log($"o <b>valor</b> da minha variavel é: <size=24>{variavel}</size> e <color=red>vermelho</color> \n " +
            $"<i><b>e formatar isso é bem chatinho</b></i>");
    }
    // Event callback example: Debug-draw all contact points and normals for 2 seconds.
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, contact.point + contact.normal, Color.green, 2, false);
        }
    }
}