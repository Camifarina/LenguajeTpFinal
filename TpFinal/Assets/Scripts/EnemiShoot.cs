using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShoot : MonoBehaviour
{
    public Transform player_pos;
    public float speed = 1f;
    public float distanciaFrenado;
    public float distanciaRetraso;

    public Transform puntoInstancia;
    public GameObject bala;
    private float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimientos
        if (player_pos.position.x < this.transform.position.x)
        {
            // Si el personaje está a la izquierda, mueve al villano hacia el personaje
            if (Vector2.Distance(transform.position, player_pos.position) > distanciaFrenado)
            {
                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player_pos.position) < distanciaRetraso)
            {
                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
            }
            else
            {
                // Si el personaje está cerca pero no en rango de frenado o retraso, el villano se queda quieto
                transform.position = transform.position;
            }

            this.transform.localScale = new Vector2(2, 2); // Villano mira a la izquierda
        }
        else
        {
            // Si el personaje está a la derecha, el villano se queda quieto
            transform.position = transform.position;

            this.transform.localScale = new Vector2(-2, 2); // Villano mira a la derecha

            // Restablecer el tiempo de disparo para que pueda disparar nuevamente cuando el personaje esté a la izquierda
            tiempo = 2f;
        }

        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && player_pos.position.y < -1)
        {
            Instantiate(bala, puntoInstancia.position, Quaternion.identity);
            tiempo = 0;
        }

    }
}
