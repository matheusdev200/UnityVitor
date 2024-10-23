//bibliotecas ou namespaces
//importar módulos de funções
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//C# Raiz vs C# Unity
//palavra-chave tipo Nome : (herança) DeQuemHerda
public class PlayerMovement : MonoBehaviour
{
    #region VARIÁVEIS
    //"Variáveis"
    [Header("Velocidade do Jogador")] //cabeçalho pras minhas variáveis
    //[Atributo]
    [SerializeField] float playerSpeed; //"serializar" é transformar em dados binários
                                        //e mandar pra outro canto
    [SerializeField] float jumpForce;
    Vector3 movementDirection; //vetor de movimento
    Rigidbody myRigidbody;

    [SerializeField] ParticleSystem jumpEffect;
    [SerializeField] ParticleSystem landEffect;

    [Header("Física do Jogador")]
    [SerializeField] float atritoComOChao = 2;
    [SerializeField] LayerMask camadaDoChao;
    [SerializeField] float distanciaDoChao;
    bool noChao = false;
    /* Escopo de Variável
    escopo = quem é dono da variável
    */

    public bool parede;
    public bool paredeNaDireita;

    #endregion

    //Métodos da Classe
    void Awake() //roda antes do Start
                 //preencher referências no Awake
    {
        myRigidbody = GetComponent<Rigidbody>(); //preencher a referência da variável
    }
    void Start() //roda ao carregar o objeto
                 //inicializar variáveis no Start
    {
        //playerSpeed = 5f;
    }
    void FixedUpdate() //construído pra rodar operações físicas
    {
        //roda a gravidade antes do MovePlayer
        if (Physics.Raycast(transform.position, -Vector3.up, distanciaDoChao, camadaDoChao)) //solta um raio na direção pra baixo
                                                                                             //e testa se encostou em algo da camada "camadaDoChao"
        {
            if (myRigidbody.velocity.y != 0f)//se eu tinha velocidade vertical (caindo ou pulando) ao tocar no chão
            {
                landEffect.Play();
            }
            noChao = true;
        }
        else
        {
            noChao = false;
        }
        if (noChao)
        {
            myRigidbody.drag = atritoComOChao; //aplica o atrito
        }
        else
        {
            myRigidbody.drag = 0f; //remove o atrito
        }
        MoverPlayer(); //calcula e aplica a força de movimento
        AjustarVelocidade(); //ajusta a velocidade
        Debug.DrawLine(new Vector3(transform.position.x, distanciaDoChao, transform.position.z), transform.position, Color.blue);
    }
    void Update() //farra do boi
    {
        //dever de casa debug.drawline feito direito :)
        //reta -> ela é infinita :)
        //segmento de reta -> um pedaço da reta infinita
        //Debug.DrawRay(transform.position, -Vector3.up, Color.green); //infinito pra baixo
        //Debug.DrawLine(new Vector3(transform.position.x, distanciaDoChao, transform.position.z), transform.position, Color.blue);
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }
    void Jump()
    {
        jumpEffect.Play();
        myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void MoverPlayer()
    {
        //0 limpar a variável
        movementDirection = Vector3.zero;
        //1 cria o vetor (pode ser uma variável lá em cima)
        //Vector3 direcaoDoMovimentoDoPlayer; // dono = MoverPlayer
        //2 constrói o vetor
        movementDirection = new Vector3(-Input.GetAxis("Vertical") * playerSpeed,
            0f, Input.GetAxis("Horizontal") * playerSpeed);
        //3 faz os cálculos necessários se precisar
        //4 aplica o vetor
        myRigidbody.AddForce(movementDirection * 10f, ForceMode.Force);
        //construir um ajuste de velocidade;
    }
    void AjustarVelocidade()
    {
        //anota quanto vale a velocidade atual do rigidbody
        Vector3 velAtual = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        if (velAtual.magnitude > playerSpeed) //verifica se ela está maior que a velocidade do player
                                              //magnitude = intensidade do vetor
        {
            //Debug.Log("meu fi tá indo muito rápido :(");
            Vector3 velCorrigida = velAtual.normalized * playerSpeed; //normalized calcula como ficaria o
                                                                      //valor solicitado numa porcentagem.
                                                                      //Ele fica contido dentro de um intervalo entre 0 e 1
            myRigidbody.velocity = new Vector3(velCorrigida.x, velCorrigida.y, velCorrigida.z);//coloca a velocidade corrigida lá.
        }

        /* Freio sem atrito
        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            //myRigidbody.velocity = Vector3.zero;
            //alterar usando coeficiente de atrito
        }*/
    }
}