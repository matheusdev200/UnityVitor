//bibliotecas ou namespaces
//importar m�dulos de fun��es
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//C# Raiz vs C# Unity
//palavra-chave tipo Nome : (heran�a) DeQuemHerda
public class PlayerMovement : MonoBehaviour
{
    #region VARI�VEIS
    //"Vari�veis"
    [Header("Velocidade do Jogador")] //cabe�alho pras minhas vari�veis
    //[Atributo]
    [SerializeField] float playerSpeed; //"serializar" � transformar em dados bin�rios
                                        //e mandar pra outro canto
    [SerializeField] float jumpForce;
    Vector3 movementDirection; //vetor de movimento
    Rigidbody myRigidbody;

    [SerializeField] ParticleSystem jumpEffect;
    [SerializeField] ParticleSystem landEffect;

    [Header("F�sica do Jogador")]
    [SerializeField] float atritoComOChao = 2;
    [SerializeField] LayerMask camadaDoChao;
    [SerializeField] float distanciaDoChao;
    bool noChao = false;
    /* Escopo de Vari�vel
    escopo = quem � dono da vari�vel
    */

    public bool parede;
    public bool paredeNaDireita;

    #endregion

    //M�todos da Classe
    void Awake() //roda antes do Start
                 //preencher refer�ncias no Awake
    {
        myRigidbody = GetComponent<Rigidbody>(); //preencher a refer�ncia da vari�vel
    }
    void Start() //roda ao carregar o objeto
                 //inicializar vari�veis no Start
    {
        //playerSpeed = 5f;
    }
    void FixedUpdate() //constru�do pra rodar opera��es f�sicas
    {
        //roda a gravidade antes do MovePlayer
        if (Physics.Raycast(transform.position, -Vector3.up, distanciaDoChao, camadaDoChao)) //solta um raio na dire��o pra baixo
                                                                                             //e testa se encostou em algo da camada "camadaDoChao"
        {
            if (myRigidbody.velocity.y != 0f)//se eu tinha velocidade vertical (caindo ou pulando) ao tocar no ch�o
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
        MoverPlayer(); //calcula e aplica a for�a de movimento
        AjustarVelocidade(); //ajusta a velocidade
        Debug.DrawLine(new Vector3(transform.position.x, distanciaDoChao, transform.position.z), transform.position, Color.blue);
    }
    void Update() //farra do boi
    {
        //dever de casa debug.drawline feito direito :)
        //reta -> ela � infinita :)
        //segmento de reta -> um peda�o da reta infinita
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
        //0 limpar a vari�vel
        movementDirection = Vector3.zero;
        //1 cria o vetor (pode ser uma vari�vel l� em cima)
        //Vector3 direcaoDoMovimentoDoPlayer; // dono = MoverPlayer
        //2 constr�i o vetor
        movementDirection = new Vector3(-Input.GetAxis("Vertical") * playerSpeed,
            0f, Input.GetAxis("Horizontal") * playerSpeed);
        //3 faz os c�lculos necess�rios se precisar
        //4 aplica o vetor
        myRigidbody.AddForce(movementDirection * 10f, ForceMode.Force);
        //construir um ajuste de velocidade;
    }
    void AjustarVelocidade()
    {
        //anota quanto vale a velocidade atual do rigidbody
        Vector3 velAtual = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        if (velAtual.magnitude > playerSpeed) //verifica se ela est� maior que a velocidade do player
                                              //magnitude = intensidade do vetor
        {
            //Debug.Log("meu fi t� indo muito r�pido :(");
            Vector3 velCorrigida = velAtual.normalized * playerSpeed; //normalized calcula como ficaria o
                                                                      //valor solicitado numa porcentagem.
                                                                      //Ele fica contido dentro de um intervalo entre 0 e 1
            myRigidbody.velocity = new Vector3(velCorrigida.x, velCorrigida.y, velCorrigida.z);//coloca a velocidade corrigida l�.
        }

        /* Freio sem atrito
        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            //myRigidbody.velocity = Vector3.zero;
            //alterar usando coeficiente de atrito
        }*/
    }
}