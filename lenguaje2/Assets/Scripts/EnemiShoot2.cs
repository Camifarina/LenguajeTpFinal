using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShoot2 : MonoBehaviour
{
    public Transform player_pos;
    public float speed = 1f;
    public float distanciaFrenado;
    public float distanciaRetraso;
    public float distanciaparaDisparar = 20f;

    public Transform controladorDisparo;
    public GameObject bala;
    private float tiempo;
    public int atacado2;
    public bool estaMuerto2;
    public int sinmascara2;
    public bool sinMascara2;

    private BoxCollider2D villainCollider;
    private Rigidbody2D rb;
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;

        estaMuerto2 = false;

        villainCollider = GetComponent<BoxCollider2D>(); //Collider del villano
        rb = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && !estaMuerto2 && !sinMascara2)
        {
            float distancia = Vector2.Distance(transform.position, player_pos.position);
            if (distancia < distanciaparaDisparar)
            {
                SoundManager.instance.PlaySound("sonidoLanzar"); // Añade esta línea
                                                                 // Calcular la dirección hacia el jugador
                Animator.SetBool("ataca", true);
                Vector3 direccionHaciaJugador = (player_pos.position - controladorDisparo.position).normalized;
                Instantiate(bala, controladorDisparo.position, Quaternion.FromToRotation(Vector3.left, direccionHaciaJugador));
                tiempo = 0;
            }
            else {
                Animator.SetBool("ataca", false);
            }
        }
        Debug.Log(tiempo);
        if (atacado2 == 1)
        {
            estaMuerto2 = true;
            villainCollider.enabled = false;
            rb.gravityScale = 0f; // Desactivar la gravedad en 2D
            Animator.SetBool("muerto", true);
        }
        if (sinmascara2 == 1)
        {
            sinMascara2 = true;
            villainCollider.enabled = false;
            rb.gravityScale = 0f;
            Animator.SetBool("sinmascara", true);
        }
    }
}
