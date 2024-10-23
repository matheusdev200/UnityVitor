using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public CanonScriptable meuScriptable;
    void Start()
    {
        Instantiate(meuScriptable.LoadCanonArt(), transform);
        gameObject.name = meuScriptable.LoadCanonName();
    }
}