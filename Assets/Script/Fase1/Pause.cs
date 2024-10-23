using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Vitor
{
    public class Pause : MonoBehaviour
    {
        public static bool pausado = false; //a variavel global de pause
        static bool myPauseController = false; //o controle interno de pause

        public Transform pontoPause;
        public Transform pontoStart;

        public PlayableDirector directorPause;
        public PlayableDirector directorStart;

        public PlayableAsset pauseAsset;
        public PlayableAsset unpauseAsset;

        public int speed;
        bool animacaoPauseOk = false;
        bool animacaoStartOk = false;

        int tamanhoMenu;

        void Start()
        {
            StartCoroutine(MenuPauseRoutine());
        }
        public static void StartAndPause()
        {
            myPauseController = !myPauseController;
            //pausado = !pausado;
        }
        IEnumerator MenuPauseRoutine()
        {
            if (myPauseController)
            {
                pausado = true; // pausa :)
                                //movimento de subida
                if (transform.position.y < pontoPause.position.y)
                {
                    animacaoStartOk = false;
                    transform.position = new Vector3
                    (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                //animacao de abertura
                if (transform.position.y >= pontoPause.position.y && !animacaoPauseOk)
                {
                    directorPause.Play();
                    animacaoPauseOk = true;
                }
            }
            if (!myPauseController)
            {
                //animacao de fechamento
                if (transform.position.y >= pontoPause.position.y && !animacaoStartOk)
                {
                    directorStart.Play();
                    animacaoStartOk = true;
                }
                yield return new WaitForSeconds((float)directorStart.duration);
                pausado = false;
                //movimento de descida
                if (transform.position.y > pontoStart.position.y && animacaoStartOk)
                {
                    transform.position = new Vector3
                    (transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                    animacaoPauseOk = false;
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(MenuPauseRoutine());
        }
        void TamanhoMenuPause()
        {
            if (tamanhoMenu == 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}