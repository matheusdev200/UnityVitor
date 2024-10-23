using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlaybChange : MonoBehaviour
{
    public PlayableAsset[] ts;
    public PlayableDirector director;

    public int t;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            director.playableAsset = ts[t];
            director.Play();
        }
    }
}