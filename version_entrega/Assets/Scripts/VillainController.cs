using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainController : MonoBehaviour
{

    public float detectionDistance = 3f; // Distancia de detección del personaje.
    public float shootingCooldown = 1f; // Tiempo entre disparos.
    public GameObject bulletPrefab; // Prefab del proyectil.
    public float moveSpeed = 2f; // Velocidad de movimiento del villano.
    public Transform shootingPoint; // Punto de disparo del villano.

    private Transform player; // Referencia al personaje.
    private float lastShotTime; // Último momento en que se disparó.
    private bool isGrounded; // Variable para verificar si el villano está en el suelo.

    private Animator anim;
    private Rigidbody2D rb;

    public Sprite normalSprite; // Sprite normal del personaje.
    public Sprite hitSprite; // Sprite después de ser alcanzado.
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del personaje.


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Buscar al personaje por etiqueta.
        lastShotTime = -shootingCooldown; // Iniciar con un tiempo negativo para disparar inmediatamente.

        anim = GetComponent<Animator>(); // Obtener la referencia al Animator.

        // Obtener la referencia al Rigidbody2D.
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Desactivar la gravedad para que el villano no caiga.

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Verificar si el villano está en el suelo (similar a GroundDetector).
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;

        if (Vector2.Distance(transform.position, player.position) <= detectionDistance)
        {
            // Calcular la dirección hacia el personaje.
            Vector2 direction = (player.position - transform.position).normalized;

            // Calcular la velocidad del villano hacia el personaje.
            Vector2 velocity = direction * moveSpeed;

            // Aplicar la velocidad al Rigidbody2D.
            rb.velocity = new Vector2(velocity.x, rb.velocity.y);

            // Girar el sprite del villano según la dirección.
            if (velocity.x > 0)
            {
                // El personaje está a la derecha, el sprite del villano debería mirar a la derecha.
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (velocity.x < 0)
            {
                // El personaje está a la izquierda, el sprite del villano debería mirar a la izquierda.
                transform.localScale = new Vector3(-1, 1, 1);

            }

            if (Time.time - lastShotTime >= shootingCooldown)
            {
                Shoot(); // Disparar hacia el personaje si se cumple el tiempo de espera.
            }

        }
        else
        {
            // Desactivar la animación de caminar.
            anim.SetBool("IsWalking", false);

            // Detener el movimiento del villano en el eje X.
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Shoot()
    {
        lastShotTime = Time.time; // Registrar el tiempo del último disparo.

        // Calcular la dirección hacia el personaje.
        Vector2 direction = (player.position - shootingPoint.position).normalized;

        // Crear un proyectil en el punto de disparo del villano.
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Asignar velocidad al proyectil en la dirección del personaje.
        bulletRb.velocity = direction * 5f; // Ajusta la velocidad según tu necesidad.

        // Reproducir sonido de lanzamiento cuando el villano dispara.
        //SoundManager.instance.PlaySound("sonidoLanzar");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            // Cambia el sprite del personaje a la apariencia alcanzada.
            spriteRenderer.sprite = hitSprite;

            // Llama a una función para restaurar el sprite después de 2 segundos.
            Invoke("RestoreNormalSprite", 2f);
        }
    }

    void RestoreNormalSprite()
    {
        // Restaura el sprite del personaje a la apariencia normal.
        spriteRenderer.sprite = normalSprite;
    }

}
