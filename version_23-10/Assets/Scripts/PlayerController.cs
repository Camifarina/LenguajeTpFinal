
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO.Ports;

public class PlayerController : MonoBehaviour
{
    //public SerialPort puerto = new SerialPort("COM5", 9600); //Descomentar cuando se conecta el arduino
    public bool izquierda = false;
    public bool derecha = false;
    public bool saltar = false;
    public bool matar = false;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private int cantidadVillanos = 7;

    private Transform mascara_suelo;
    private Mascara_suelo mascara_s;

    private Transform flor_pos; //Flor
    private Flor flor = new Flor();
    private float distanciaFlor = new float();

    private Transform[] villano_pos = new Transform[7]; //Enemigos
    public float distanciaParaMatar = 5f;
    private float[] distancia = new float[7];
    private EnemiShoot[] enemigo = new EnemiShoot[7];
    public bool eSinMascara = false;
    public bool eMuerto = false;
    public Transform controladorSenal;
    public GameObject senal_izq;
    public Transform controladorSenal2;
    public GameObject senal_der;
    public GameObject recuadro_golpeado; //recuadros según lo que pasa en el juego
    public GameObject recuadro_liberoCiudadano;
    public GameObject recuadro_matoCiudadano;
    public Transform controladorRecuadro; //controlador de los recuadros



    private Rigidbody2D rb;

    private int colisiones = 0; // Contador de colisiones.
    public int vidas = 3; // Número máximo de colisiones antes de perder.
    public int vSinMasc = 0;
    private int vMuertos = 0;
    public bool mareado = false;
    public bool atrapado = false;
    public float tiempoMuerto = 0;
    public float time = 0;
    public bool tirarSenal = false; //booleano señales para liberar
    public bool conFlor = false; //booleano flor

    private Animator Animator;
    private bool isWalking = false; // Variable para verificar si el personaje está caminando.
    public bool isGrounded;

    /* --- SONIDOS --- */
    public AudioClip pasosSound; // Sonido de pasos.
    public AudioClip salto; // Sonido de salto.
    public AudioClip espada; // Sonido de espada.
    public AudioClip quitar_mascara; // Sonido de cuando saca las mascaras.
    public AudioClip raulGolpeado; 
    private bool espadaSoundPlayed = false;
    private bool quitarMascaraSoundPlayed = false;
    private AudioSource audioPasos; // Referencia al AudioSource para el sonido de pasos.
    public GameObject sonidoVillanoMuerto;
    public GameObject risaMalo;
    public GameObject sonidoFondoJuego;
    public GameObject sonidoFlor;
    private bool sonido_Flor = false;
    private bool antesHabiaSonidoFlor = false;
    public GameObject sonidoCharco;
    private bool sonido_Charco = false;
    private bool antesHabiaSonidoCharco = false;


    private void Start()
    {
        // puerto.ReadTimeout = 30;
        // puerto.Open();  //Descomentar cuando se conecta el arduino
        rb = GetComponent<Rigidbody2D>();

        villano_pos[0] = GameObject.Find("Villain").transform;
        villano_pos[1] = GameObject.Find("Villain2").transform;
        villano_pos[2] = GameObject.Find("Villain3").transform;
        villano_pos[3] = GameObject.Find("Villain4").transform;
        villano_pos[4] = GameObject.Find("Villain5").transform;
        villano_pos[5] = GameObject.Find("Villain6").transform;
        villano_pos[6] = GameObject.Find("malo").transform;
        for (int i = 0; i < cantidadVillanos; i++)
        {
            enemigo[i] = villano_pos[i].GetComponent<EnemiShoot>();
        }

        mascara_suelo = GameObject.Find("mascara_suelo").transform;
        mascara_s = mascara_suelo.GetComponent<Mascara_suelo>();

        flor_pos = GameObject.Find("Flor").transform;
        flor = flor_pos.GetComponent<Flor>();


        Animator = GetComponent<Animator>();

        /* --- SONIDOS ---  */
        audioPasos = GetComponent<AudioSource>();
        audioPasos.clip = pasosSound;
        audioPasos.loop = false; // Desactiva el bucle inicialmente.

        Instantiate(sonidoFondoJuego);

    }

    private void Update()
    {
        /* try
        {
            if (puerto.IsOpen)
            {
                string dato_recibido = puerto.ReadLine();
                if (dato_recibido.Equals("I"))
                {
                    izquierda = true;
                }
                else if (dato_recibido.Equals("D"))
                {
                    derecha = true;
                }
                else if (dato_recibido.Equals("S"))
                {
                    saltar = true;
                }
                else if (dato_recibido.Equals("A"))
                {
                    matar = true;
                }
            }
        }
        catch (System.Exception ex1)
        {
            ex1 = new System.Exception();
        }  //Descomentar cuando se conecta el arduino  */

        // Mover a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || izquierda)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-2, 2, 2);
            TryPlayFootstepSound();
            if (isGrounded)
            {
                Animator.SetBool("Camina", true);
            }
        }
        // Mover a la derecha
        else if (Input.GetKey(KeyCode.RightArrow) || derecha)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(2, 2, 2);
            TryPlayFootstepSound();
            if (isGrounded)
            {
                Animator.SetBool("Camina", true);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            Animator.SetBool("Camina", false);
            StopFootstepSound();
        }

        // Saltar solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || saltar && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Animator.SetBool("Salta", true);
            isGrounded = false;

            if (salto != null)
            {
                audioPasos.PlayOneShot(salto);
            }
        }
        else
        {
            Animator.SetBool("Salta", false);
        }

        for (int i = 0; i < cantidadVillanos; i++)
        {
            distancia[i] = Vector2.Distance(transform.position, villano_pos[i].position);
        }

        distanciaFlor = Vector2.Distance(transform.position, flor_pos.position); //DISTANCIA FLOR

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A) || matar)
        {
            Animator.SetBool("Mata", true);

            for (int i = 0; i < cantidadVillanos; i++)
            {
                if (villano_pos[i].position.x > this.transform.position.x)
                {
                    if (distancia[i] < distanciaParaMatar && enemigo[i].sinMascara == false && enemigo[i].estaMuerto == false)
                    {
                        if (enemigo[i] != null)
                        {
                            enemigo[i].estaMuerto = true;
                            eMuerto = true;
                            Instantiate(sonidoVillanoMuerto);

                        }
                    }
                }
            }
            if (!espadaSoundPlayed && espada != null)
            {
                audioPasos.PlayOneShot(espada);
                espadaSoundPlayed = true; // Marca el sonido como reproducido
                
                //audioPasos.PlayOneShot(villanoMuerto);
            }
        }
        else
        {
            Animator.SetBool("Mata", false);
            espadaSoundPlayed = false; // Restablece la bandera cuando se suelta la tecla
        }
        Debug.Log("colisiones: " + colisiones);
        //Debug.Log("sonido: " + sonidoVillanoMuerto);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            for (int i = 0; i < cantidadVillanos; i++)
            {
                if (villano_pos[i].position.x > this.transform.position.x)
                {
                    if (distancia[i] < distanciaParaMatar && enemigo[i].sinMascara == false && enemigo[i].estaMuerto == false)
                    {
                        if (enemigo[i] != null)
                        {
                            transform.localScale = new Vector3(2, 2, 2);
                            Animator.SetBool("sacamascara", true);
                            enemigo[i].sinMascara = true;
                        }
                        if (!quitarMascaraSoundPlayed && quitar_mascara != null)
                        {
                            audioPasos.PlayOneShot(quitar_mascara);
                            quitarMascaraSoundPlayed = true;
                            eSinMascara = true;
                        }
                    }
                }
            }
            if (flor_pos.position.x > this.transform.position.x)
            {
                if (distanciaFlor < distanciaParaMatar)
                {
                    conFlor = true;
                    Animator.SetBool("sacamascara", true);
                    flor.florInactiva = true;
                    sonido_Flor = true;
                }
            }
        }
        else
        {
            Animator.SetBool("sacamascara", false);
            quitarMascaraSoundPlayed = false;
        }

        if (!antesHabiaSonidoFlor && sonido_Flor)
        {
            Instantiate(sonidoFlor);
        }
        antesHabiaSonidoFlor = sonido_Flor;

        if (eSinMascara == true)
        {
            Instantiate(recuadro_liberoCiudadano, controladorRecuadro.position, Quaternion.identity);
            vSinMasc = vSinMasc + 1;
            eSinMascara = false;
            
        }
        if (eMuerto == true)
        {
            Instantiate(recuadro_matoCiudadano, controladorRecuadro.position, Quaternion.identity);
            vMuertos = vMuertos + 1;
            eMuerto = false;
            
        }


        if (distanciaFlor < distanciaParaMatar)
        {
            Instantiate(senal_izq, controladorSenal.position, Quaternion.identity);
            Instantiate(senal_der, controladorSenal2.position, Quaternion.identity);
        }

        //Debug.Log(isGrounded);
        Debug.Log("villanos liberados: " + vSinMasc);
        Debug.Log("villanos muertos: " + vMuertos);

        if (vSinMasc >= cantidadVillanos)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            time += Time.deltaTime;
            if (time >= 3)
            {
                Ganar();
            }
        }

        if (atrapado)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            Animator.SetBool("muerto", true);
            tiempoMuerto += Time.deltaTime;
            if (tiempoMuerto >= 2)
            {
                //Si el contador alcanza el máximo, llama a la función de muerte.
                Die();
            }
        }
        if (mareado && !atrapado)
        {
            Instantiate(recuadro_golpeado, controladorRecuadro.position, Quaternion.identity);
            Animator.SetBool("mareado", true);
            mareado = false;
        }
        else
        {
            Animator.SetBool("mareado", false);
        }

        for (int i = 0; i < cantidadVillanos; i++)
        {
            if (distancia[i] < distanciaParaMatar && enemigo[i].sinMascara == false && enemigo[i].estaMuerto == false)
            {
                Instantiate(senal_izq, controladorSenal.position, Quaternion.identity);
                Instantiate(senal_der, controladorSenal2.position, Quaternion.identity);
            }
        }

        izquierda = false;
        derecha = false;
        matar = false;
        saltar = false;

        if ((enemigo[6].sinMascara == true || enemigo[6].estaMuerto == true) && vMuertos <= cantidadVillanos && vMuertos > 0)
        {
            tiempoMuerto += Time.deltaTime;
            if (tiempoMuerto >= 3)
            {
                Die();
            }
        }

        if (conFlor)
        {
            time += Time.deltaTime;
            flor.florInactiva = true;
            if (time >= 10)
            {
                conFlor = false;
                time = 0;
            }
        }
    }

    private void TryPlayFootstepSound()
    {
        if (!isWalking)
        {
            isWalking = true;
            StartCoroutine(PlayFootstepSound());
        }
    }

    private void StopFootstepSound()
    {
        if (isWalking)
        {
            isWalking = false;
            audioPasos.Stop();
        }
    }

    private IEnumerator PlayFootstepSound()
    {
        audioPasos.Play();

        while (isWalking)
        {
            yield return null;
        }

        audioPasos.Stop();
    }


    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Bala") && !conFlor)
        {
            colisiones++; // Incrementa el contador de colisiones.
            mareado = true;
            audioPasos.PlayOneShot(raulGolpeado);
            if (colisiones >= vidas)
            {
                atrapado = true;
            }
        }


        if (colision.gameObject.CompareTag("Ground")) // Asegúrate de que el tag del suelo sea "Ground".
        {
            isGrounded = true;
        }

        if (colision.gameObject.CompareTag("mascara_suelo") && !conFlor)
        {
            mascara_s.toco_mascara = true;
            colisiones++;
            mareado = true;
            sonido_Charco = true;
            if (colisiones >= vidas)
            {
                atrapado = true;
            }
        }

        if (!antesHabiaSonidoCharco && sonido_Charco)
        {
            Instantiate(sonidoCharco);
        }
        antesHabiaSonidoCharco = sonido_Charco;


        if (colision.gameObject.CompareTag("Flor"))
        {
            flor.florDesactivada = true;
        }
    }

    void Die()
    {
        SceneManager.LoadScene(3);
    }
    void Ganar()
    {
        SceneManager.LoadScene(2);
    }
}