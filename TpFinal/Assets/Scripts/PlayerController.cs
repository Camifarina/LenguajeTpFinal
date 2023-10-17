using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class PlayerController : MonoBehaviour
{
    public SerialPort puerto = new SerialPort("COM3", 9600);
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform villano_pos; //Villano
    public float distanciaParaMatar = 5f;
    public EnemiShoot enemigo;

    private Rigidbody2D rb;

    private int colisiones = 0; // Contador de colisiones.
    public int vidas = 3; // Número máximo de colisiones antes de perder.

    private Animator Animator;

    public bool isGrounded;

    private void Start()
    {
        puerto.ReadTimeout = 30;
        puerto.Open();
        rb = GetComponent<Rigidbody2D>();

        villano_pos = GameObject.Find("Villain").transform; //Villano
        enemigo = villano_pos.GetComponent<EnemiShoot>();

        Animator = GetComponent<Animator>();
    }

    private void Update()
    {


            // Mover a la izquierda
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector3(-2, 2, 2);
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
                if (isGrounded)
                {
                    Animator.SetBool("Camina", true);
                }
            }
            else
            {
                Animator.SetBool("Camina", false);
            }
            // Saltar solo si está en el suelo
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Animator.SetBool("Salta", true);
                isGrounded = false;
            }
            else
            {
                Animator.SetBool("Salta", false);
            }

            if (villano_pos.position.x > this.transform.position.x)
            {
                float distancia = Vector2.Distance(transform.position, villano_pos.position);

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A))
                {
                    Animator.SetBool("Mata", true);
                    if (distancia < distanciaParaMatar)
                    {
                        if (enemigo != null)
                        {
                            Debug.Log("El villano está muerto");
                            enemigo.atacado = 1;
                        }
                        else
                        {
                            Debug.Log("Enemigo es null");
                        }
                    }
                }
                else
                {
                    Animator.SetBool("Mata", false);
                }
                Debug.Log(colisiones);
            }
            Debug.Log(isGrounded);
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
            }
        }

        void Die()
        {
            // Carga la escena de Game Over y reinicio
            SceneManager.LoadScene(3);
        }
    }