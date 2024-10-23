using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateCanvasShader : MonoBehaviour
{
    public bool active;
    public float zoomFade = -5f;
    Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(UpdateScreenFadeRoutine());
    }
    IEnumerator UpdateScreenFadeRoutine()
    {
        myImage.material.SetFloat("_Zoom_Amount", zoomFade);
        yield return new WaitForSeconds(Time.deltaTime);
        if (active)
        {
            StartCoroutine(UpdateScreenFadeRoutine());
        }
    }
}