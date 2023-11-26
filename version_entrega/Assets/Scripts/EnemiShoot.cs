
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShoot : MonoBehaviour
{
    public Transform player_pos;
    public PlayerController raulcito;
    public float speed = 1f;
    public float distanciaFrenado;
    public float distanciaRetraso;
    public float distanciaparaDisparar = 20f;
    private int valor;

    public Transform controladorDisparo;
    public GameObject bala;
    public GameObject sonidoEnojado;
    public GameObject sonidoTriste;
    public GameObject sonidoFeliz;
    public GameObject sacarMasc;
    public GameObject lanzarMasc;

    private float tiempo;
    public bool estaMuerto = false;
    public bool sinMascara;
    private bool emocionesCambio = false;
    private bool antesHabiaEmociones = false;
    private bool sonidoSinMasc = false;
    private bool antesSonidoSinMasc = false;

    private BoxCollider2D villainCollider;
    private Rigidbody2D rb;
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
        raulcito = player_pos.GetComponent<PlayerController>();

        villainCollider = GetComponent<BoxCollider2D>(); //Collider del villano
        rb = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();

        valor = Random.Range(1, 4);
        Animator.SetInteger("emociones", valor);
    }

    // Update is called once per frame
    void Update()
    {
        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 4 && player_pos.position.x < this.transform.position.x && raulcito.isGrounded && !estaMuerto && !sinMascara)
        {
            float distancia = Vector2.Distance(transform.position, player_pos.position);
            if (distancia < distanciaparaDisparar)
            {
                Instantiate(lanzarMasc);
                Animator.SetBool("ataca", true);
                Vector3 direccionHaciaJugador = (player_pos.position - controladorDisparo.position).normalized;
                Instantiate(bala, controladorDisparo.position, Quaternion.FromToRotation(Vector3.left, direccionHaciaJugador));
                tiempo = 0;
            }
        }
        else
        {
            Animator.SetBool("ataca", false);
        }



        if (estaMuerto)
        {
            villainCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f; // Desactivar la gravedad en 2D
            Animator.SetBool("muerto", true);
        }
        if (sinMascara)
        {
            villainCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f;
            Animator.SetBool("sinmascara", true);
            sonidoSinMasc = true;
            emocionesCambio = true;


            if (valor == 1 && !antesHabiaEmociones && emocionesCambio)
            {
                Instantiate(sonidoEnojado);

            }
            else if (valor == 2 && !antesHabiaEmociones && emocionesCambio)
            {
                Instantiate(sonidoFeliz);

            }
            else if (valor == 3 && !antesHabiaEmociones && emocionesCambio)
            {
                Instantiate(sonidoTriste);

            }

            antesHabiaEmociones = emocionesCambio;

        }

        if (!antesSonidoSinMasc && sonidoSinMasc)
        {
            Instantiate(sacarMasc);
        }
        antesSonidoSinMasc = sonidoSinMasc;

    }
}
