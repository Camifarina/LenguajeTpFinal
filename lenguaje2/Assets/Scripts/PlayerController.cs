
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public AudioClip pasosSound; // Sonido de pasos.
    public AudioClip salto; // Sonido de salto.
    public AudioClip espada; // Sonido de espada.
    public AudioClip quitar_mascara; // Sonido de cuando saca las mascaras.
    private bool espadaSoundPlayed = false;
    private bool quitarMascaraSoundPlayed = false;

    public Transform villano_pos; //Villano
    public Transform villano_pos2;
    public float distanciaParaMatar = 5f;
    public float distancia;
    public float distancia2;
    public EnemiShoot enemigo;
    public EnemiShoot2 enemigo2;


    private Rigidbody2D rb;

    private int colisiones = 0; // Contador de colisiones.
    public int vidas = 3; // Número máximo de colisiones antes de perder.
    public int vSinMasc = 0;

    private Animator Animator;

    private bool isWalking = false; // Variable para verificar si el personaje está caminando.
    private AudioSource audioPasos; // Referencia al AudioSource para el sonido de pasos.
    // private AudioSource audioSalto;
    // private AudioSource audioEspada;
    // private AudioSource audioMascara;


    public bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        villano_pos = GameObject.Find("Villain").transform; //Villano
        enemigo = villano_pos.GetComponent<EnemiShoot>();

        villano_pos2 = GameObject.Find("Villain2").transform; //Villano2
        enemigo2 = villano_pos2.GetComponent<EnemiShoot2>();

        Animator = GetComponent<Animator>();

        /* --- SONIDOS ---  */
        audioPasos = GetComponent<AudioSource>();
        audioPasos.clip = pasosSound;
        audioPasos.loop = false; // Desactiva el bucle inicialmente.

        SoundManager.instance.PlayBackgroundMusic("sonidoAmbiente2");

    }

    private void Update()
    {
        // Mover a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
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
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(2, 2, 2);
            TryPlayFootstepSound();
            if (isGrounded)
            {
                Animator.SetBool("Camina", true);
                SoundManager.instance.PlaySound("pasosEnLaNieve");
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            Animator.SetBool("Camina", false);
            StopFootstepSound();
        }

        // Saltar solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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

        distancia = Vector2.Distance(transform.position, villano_pos.position);
        distancia2 = Vector2.Distance(transform.position, villano_pos2.position);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("Mata", true);
            if (villano_pos.position.x > this.transform.position.x || villano_pos2.position.x > this.transform.position.x)
            {
                if (distancia < distanciaParaMatar)
                {
                    if (enemigo != null)
                    {
                        enemigo.atacado = 1;
                    }
                }
                if (distancia2 < distanciaParaMatar)
                {
                    if (enemigo2 != null)
                    {
                        enemigo2.atacado2 = 1;
                    }
                }
            }
            if (!espadaSoundPlayed && espada != null)
            {
                audioPasos.PlayOneShot(espada);
                espadaSoundPlayed = true; // Marca el sonido como reproducido
            }
        }
        else
        {
            Animator.SetBool("Mata", false);
            espadaSoundPlayed = false; // Restablece la bandera cuando se suelta la tecla
        }
        Debug.Log(colisiones);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            if (villano_pos.position.x > this.transform.position.x || villano_pos2.position.x > this.transform.position.x)
            {
                if (distancia < distanciaParaMatar)
                {
                    if (enemigo != null)
                    {
                        transform.localScale = new Vector3(2, 2, 2);
                        Animator.SetBool("sacamascara", true);
                        enemigo.sinmascara = 1;
                    }
                    if (!quitarMascaraSoundPlayed && quitar_mascara != null)
                    {
                        audioPasos.PlayOneShot(quitar_mascara);
                        quitarMascaraSoundPlayed = true; // Marca el sonido como reproducido
                        vSinMasc++;
                    }
                }
                if (distancia2 < distanciaParaMatar)
                {
                    if (enemigo2 != null)
                    {
                        transform.localScale = new Vector3(2, 2, 2);
                        Animator.SetBool("sacamascara", true);
                        enemigo2.sinmascara2 = 1;
                    }
                    if (!quitarMascaraSoundPlayed && quitar_mascara != null)
                    {
                        audioPasos.PlayOneShot(quitar_mascara);
                        quitarMascaraSoundPlayed = true; // Marca el sonido como reproducido
                        vSinMasc++;
                    }
                }

            }
        }
        else
        {
            Animator.SetBool("sacamascara", false);
            quitarMascaraSoundPlayed = false; // Restablece la bandera cuando se suelta alguna de las teclas
        }

        Debug.Log(isGrounded);

        if (vSinMasc >= 2)
        {
            Ganar();
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
        if (colision.gameObject.CompareTag("Bala"))
        {
            colisiones++; // Incrementa el contador de colisiones.

            if (colisiones >= vidas)
            {
                //Si el contador alcanza el máximo, llama a la función de muerte.
                Die();
            }
        }

        if (colision.gameObject.CompareTag("Ground")) // Asegúrate de que el tag del suelo sea "Ground".
        {
            isGrounded = true;
            //audioSalto.Stop();
        }
        else
        {
            //audioSalto.Play();
        }
    }

    void Die()
    {
        // Detén la música de fondo antes de cargar la escena de Game Over y reinicio.
        SoundManager.instance.StopBackgroundMusic();
        SoundManager.instance.hayMusicaDeFondo = false;
        SceneManager.LoadScene(3);
    }
    void Ganar()
    {
        SoundManager.instance.StopBackgroundMusic();
        SoundManager.instance.hayMusicaDeFondo = false;
        SceneManager.LoadScene(2);
    }
}
