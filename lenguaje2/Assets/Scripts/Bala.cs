using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadBala = 4f; // Velocidad de la bala.
    private Transform player; // Referencia al personaje.
    private Vector2 direccion; // Dirección hacia el personaje.
    private int colisiones = 0;
    private int tiempoMareado = 0; // Contador de tiempo

    void Start()
    {
        player = GameObject.Find("Player").transform; // Buscar al personaje por nombre.
        direccion = (player.position - transform.position).normalized;
    }

    void Update()
    {
        // Mover la bala en la dirección calculada.
transform.Translate(direccion * velocidadBala * Time.deltaTime);

        // Destruir la bala si está fuera de la pantalla o colisiona con algo.
        if (IsOutOfScreen())
        {
            Destroy(gameObject);
        }

        Debug.Log(colisiones);
    }

    bool IsOutOfScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height;
    }

    // Agrega aquí el código para detectar colisiones con el personaje u otros objetos si es necesario.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            colisiones++;
            tiempoMareado++;
        }
    }
}
