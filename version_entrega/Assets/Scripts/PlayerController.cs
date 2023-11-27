
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO.Ports;   //Descomentar cuando se conecta el arduino

public class PlayerController : MonoBehaviour
{
    //public SerialPort puerto = new SerialPort("COM5", 9600); //Descomentar cuando se conecta el arduino
    public bool izquierda = false;
    public bool derecha = false;
    public bool saltar = false;
    public bool matar = false;
    public bool sacarMascara = false;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private int cantidadVillanos = 6;

    private Transform mascara_suelo;
    private Mascara_suelo mascara_s;

    private Transform flor_pos; //Flor
    private Flor flor = new Flor();
    private float distanciaFlor = new float();

    private Transform malo_pos;
    private Malo malo = new Malo();
    private float distanciaMalo = new float();

    private Transform[] villano_pos = new Transform[6]; //Enemigos
    public float distanciaParaMatar = 5f;
    private float[] distancia = new float[6];
    private EnemiShoot[] enemigo = new EnemiShoot[6];

    public bool eSinMascara = false;
    public bool eMuerto = false;
    public bool maloMuerto = false;
    private bool teclaAPresionada = false;
    private bool teclaSDPresionada = false;
    public bool mSinMascara = false;

    public Transform controladorSenal;
    public GameObject senal_izq;
    public Transform controladorSenal2;
    public GameObject senal_der;
    public GameObject recuadro_golpeado; //recuadros según lo que pasa en el juego
    public GameObject recuadro_liberoCiudadano;
    public GameObject recuadro_matoCiudadano;
    public Transform controladorRecuadro; //controlador de los recuadros
    public GameObject burbuja; //burbuja cuando agarra la flor
    public Transform controladorBurbuja;



    private Rigidbody2D rb;

    private int colisiones = 0;
    public int vidas = 3; // Número máximo de colisiones antes de perder.
    public int vSinMasc = 0;
    private int vMuertos = 0;
    public bool mareado = false;
    public bool atrapado = false;
    public float tiempoMuerto = 0;
    public float time = 0;
    public bool tirarSenal = false; //booleano señales para liberar
    public bool conFlor = false; //booleano flor
    public int maloMuere = 0;
    public int mSinMasc = 0;


    private Animator Animator;
    private bool isWalking = false;
    public bool isGrounded;


    /* --- SONIDOS --- */
    public AudioClip pasosSound;
    public AudioClip salto;
    public AudioClip espada;
    public AudioClip quitar_mascara;
    public AudioClip raulGolpeado;
    private bool espadaSoundPlayed = false;
    private bool quitarMascaraSoundPlayed = false;
    private AudioSource audioPasos;
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
        //puerto.ReadTimeout = 30;   //Descomentar cuando se conecta el arduino
        //puerto.Open();  //Descomentar cuando se conecta el arduino
        rb = GetComponent<Rigidbody2D>();

        villano_pos[0] = GameObject.Find("Villain").transform;
        villano_pos[1] = GameObject.Find("Villain2").transform;
        villano_pos[2] = GameObject.Find("Villain3").transform;
        villano_pos[3] = GameObject.Find("Villain4").transform;
        villano_pos[4] = GameObject.Find("Villain5").transform;
        villano_pos[5] = GameObject.Find("Villain6").transform;
        for (int i = 0; i < cantidadVillanos; i++)
        {
            enemigo[i] = villano_pos[i].GetComponent<EnemiShoot>();
        }

        malo_pos = GameObject.Find("malo").transform;
        malo = malo_pos.GetComponent<Malo>();

        mascara_suelo = GameObject.Find("mascara_suelo").transform;
        mascara_s = mascara_suelo.GetComponent<Mascara_suelo>();

        flor_pos = GameObject.Find("Flor").transform;
        flor = flor_pos.GetComponent<Flor>();


        Animator = GetComponent<Animator>();

        /* --- SONIDOS ---  */
        audioPasos = GetComponent<AudioSource>();
        audioPasos.clip = pasosSound;
        audioPasos.loop = false;

        Instantiate(sonidoFondoJuego);

    }

    private void Update()
    {
        /* //Descomentar cuando se conecta el arduino desde acá
        try
        {
            if (puerto.IsOpen)
            {
                string dato_recibido = puerto.ReadLine();
                if (dato_recibido.Equals("izquierda"))
                {
                    izquierda = true;
                }
                else if (dato_recibido.Equals("abajo"))
                {
                    derecha = true;
                }
                else if (dato_recibido.Equals("arriba"))
                {
                    saltar = true;
                }
                else if (dato_recibido.Equals("A"))
                {
                    matar = true;
                }
                else if (dato_recibido.Equals("S"))
                {
                    sacarMascara = true;
                }
            }
        }
        catch (System.Exception ex1)
        {
            ex1 = new System.Exception();
        }  
        //Descomentar cuando se conecta el arduino hasta acá */


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
        distanciaMalo = Vector2.Distance(transform.position, malo_pos.position); //DISTANCIA MALO MAS MALO


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A) || matar)
        {
            if (!teclaAPresionada)
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
                if (malo_pos.position.x > this.transform.position.x && distanciaMalo < distanciaParaMatar && malo.sinMascaraMalo == false && malo.estaMuertoMalo == false)
                {
                    maloMuerto = true;
                    Instantiate(sonidoVillanoMuerto);
                }
                teclaAPresionada = true;
            }
            if (!espadaSoundPlayed && espada != null)
            {
                audioPasos.PlayOneShot(espada);
                espadaSoundPlayed = true;
            }
        }
        else
        {
            Animator.SetBool("Mata", false);
            espadaSoundPlayed = false;
            teclaAPresionada = false;
        }


        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) || sacarMascara)
        {
            if (!teclaSDPresionada)
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
                if (malo_pos.position.x > this.transform.position.x && distanciaMalo < distanciaParaMatar && malo.sinMascaraMalo == false && malo.estaMuertoMalo == false)
                {
                    if (distanciaMalo < distanciaParaMatar)
                    {
                        mSinMascara = true;
                    }
                    teclaSDPresionada = true;
                }
            }
        }
        else
        {
            Animator.SetBool("sacamascara", false);
            quitarMascaraSoundPlayed = false;
            teclaSDPresionada = false;
        }

        if (maloMuerto == true)
        {
            Instantiate(recuadro_matoCiudadano, controladorRecuadro.position, Quaternion.identity);
            maloMuere++;
            maloMuerto = false;
        }

        if (mSinMascara == true)
        {
            Instantiate(recuadro_liberoCiudadano, controladorRecuadro.position, Quaternion.identity);
            mSinMasc++;
            mSinMascara = false;
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
        if (mSinMasc >= 3)
        {
            malo.sinMascaraMalo = true;
        }

        if (distanciaFlor < distanciaParaMatar)
        {
            Instantiate(senal_izq, controladorSenal.position, Quaternion.identity);
            Instantiate(senal_der, controladorSenal2.position, Quaternion.identity);
            controladorSenal.position -= new Vector3(Random.Range(-0.01f, 0.01f), 0f, 0f);
            controladorSenal2.position -= new Vector3(Random.Range(-0.01f, 0.01f), 0f, 0f);
        }

        if (vSinMasc >= cantidadVillanos && malo.sinMascaraMalo == true)
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
                controladorSenal.position -= new Vector3(Random.Range(-0.01f, 0.01f), 0f, 0f);
                controladorSenal2.position -= new Vector3(Random.Range(-0.01f, 0.01f), 0f, 0f);
            }
        }

        izquierda = false;
        derecha = false;
        matar = false;
        saltar = false;
        sacarMascara = false;

        if (maloMuere >= 3)
        {
            malo.estaMuertoMalo = true;
        }

        if ((malo.sinMascaraMalo == true || malo.estaMuertoMalo == true) && vMuertos <= cantidadVillanos && vMuertos > 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            tiempoMuerto += Time.deltaTime;
            if (tiempoMuerto >= 3)
            {
                pierdeMato();
            }
        }

        if (conFlor)
        {
            Instantiate(burbuja, controladorBurbuja.position, Quaternion.identity);
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
            colisiones++;
            mareado = true;
            audioPasos.PlayOneShot(raulGolpeado);
            if (colisiones >= vidas)
            {
                atrapado = true;
            }
        }


        if (colision.gameObject.CompareTag("Ground"))
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

    void pierdeMato()
    {
        SceneManager.LoadScene(3);
    }
    void Ganar()
    {
        SceneManager.LoadScene(2);
    }
    void Die()
    {
        SceneManager.LoadScene(4);
    }
}
