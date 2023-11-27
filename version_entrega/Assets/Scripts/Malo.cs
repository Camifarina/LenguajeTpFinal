using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malo : MonoBehaviour
{
    public Transform player_pos;
    public PlayerController raulcito;
    public float speed = 1f;
    public float distanciaFrenado;
    public float distanciaRetraso;
    public float distanciaparaDisparar = 20f;

    public Transform controladorDisparo;
    public GameObject bala;
    private float tiempo;
    public bool estaMuertoMalo = false;
    public bool sinMascaraMalo;
    private bool emocionesCambio = false;
    private int valor = 1;
    private bool antesHabiaEmociones = false;
    public GameObject sonidoTriste;

    private BoxCollider2D villainCollider;
    private Rigidbody2D rb;
    private Animator Animator;

    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
        raulcito = player_pos.GetComponent<PlayerController>();

        villainCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && raulcito.isGrounded && !estaMuertoMalo && !sinMascaraMalo)
        {
            float distancia = Vector2.Distance(transform.position, player_pos.position);
            if (distancia < distanciaparaDisparar)
            {
                SoundManager.instance.PlaySound("sonidoLanzar");
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

        if (estaMuertoMalo)
        {
            villainCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f; // Desactivar la gravedad en 2D
            Animator.SetBool("muerto", true);
        }
        if (sinMascaraMalo)
        {
            villainCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f;
            Animator.SetBool("sinmascara", true);
            Animator.SetInteger("emociones", valor);
            //Instantiate(sonidoTriste);
            emocionesCambio = true;


            if (valor == 1 && !antesHabiaEmociones && emocionesCambio)
            {
                Instantiate(sonidoTriste);
            }
            antesHabiaEmociones = emocionesCambio;
        }
    }
}
