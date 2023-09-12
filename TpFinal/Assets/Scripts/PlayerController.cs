using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private GroundDetector groundDetector; // Referencia al detector de suelo.

    private int colisiones = 0; // Contador de colisiones.
    public int vidas = 3; // Número máximo de colisiones antes de perder.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<GroundDetector>(); // Obtén la referencia al detector de suelo.
    }

    private void Update()
    {
        // Mover a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        // Mover a la derecha
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Saltar solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Debug.Log(colisiones);
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Bala"))
        {
            colisiones++; // Incrementa el contador de colisiones.

            if (colisiones >= vidas)
            {
                // Si el contador alcanza el máximo, llama a la función de muerte.
                Die();
            }
        }
    }

    void Die()
    {
        // Carga la escena de Game Over y reinicio
        SceneManager.LoadScene(3);
    }
}
