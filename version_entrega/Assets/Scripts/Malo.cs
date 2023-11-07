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
    public bool estaMuerto = false;
    public bool sinMascara;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && raulcito.isGrounded && !estaMuerto && !sinMascara)
        {
            float distancia = Vector2.Distance(transform.position, player_pos.position);
            if (distancia < distanciaparaDisparar)
            {
                SoundManager.instance.PlaySound("sonidoLanzar"); // Añade esta línea
                Animator.SetBool("ataca", true);
                // Calcular la dirección hacia el jugador
                Vector3 direccionHaciaJugador = (player_pos.position - controladorDisparo.position).normalized;
                Instantiate(bala, controladorDisparo.position, Quaternion.FromToRotation(Vector3.left, direccionHaciaJugador));
                tiempo = 0;
            }
        }
        else
        {
            Animator.SetBool("ataca",false);
        }
        
        //Debug.Log(tiempo);
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
            //SoundManager.instance.PlaySound("efectoMareado");
            Animator.SetInteger("emociones", Mathf.RoundToInt(Random.Range(1, 4)));
        }
    }
}
