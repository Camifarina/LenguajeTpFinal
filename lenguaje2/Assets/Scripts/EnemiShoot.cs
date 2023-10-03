using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShoot : MonoBehaviour
{
    public Transform player_pos;
    public float speed = 1f;
    public float distanciaFrenado;
    public float distanciaRetraso;

    public Transform controladorDisparo;
    public GameObject bala;
    private float tiempo;
    public int atacado;
    public bool estaMuerto;

    private BoxCollider2D villainCollider;
    private Rigidbody2D rb;
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;

        estaMuerto = false;

        villainCollider = GetComponent<BoxCollider2D>(); //Collider del villano
        rb = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && !estaMuerto)
        {
            SoundManager.instance.PlaySound("sonidoLanzar"); // Añade esta línea
            // Calcular la dirección hacia el jugador
            Vector3 direccionHaciaJugador = (player_pos.position - controladorDisparo.position).normalized;
            Instantiate(bala, controladorDisparo.position, Quaternion.FromToRotation(Vector3.left, direccionHaciaJugador));
            tiempo = 0;
        }
        Debug.Log(tiempo);
        if (atacado == 1) {
            estaMuerto = true;
            villainCollider.enabled = false;
            rb.gravityScale = 0f; // Desactivar la gravedad en 2D
            Animator.SetBool("muerto", true);
        }
    }
}
