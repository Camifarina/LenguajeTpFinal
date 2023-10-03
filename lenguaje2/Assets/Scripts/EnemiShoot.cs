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
    public int atacado;
    public bool estaMuerto;

    private bool haRotado90Grados = false;


    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;

        estaMuerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimientos
        if (player_pos.position.x < this.transform.position.x)
        {
            
            // Si el personaje est� a la izquierda, mueve al villano hacia el personaje
            if (Vector2.Distance(transform.position, player_pos.position) > distanciaFrenado && !estaMuerto)
            {
                transform.position = transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player_pos.position) < distanciaRetraso)
            {
                transform.position = transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
            }

            this.transform.localScale = new Vector2(2, 2); // Villano mira a la izquierda
            
        }
        else
        {
            // Si el personaje est� a la derecha, el villano se queda quieto
            transform.position = transform.position;

            this.transform.localScale = new Vector2(-2, 2); // Villano mira a la derecha

            // Restablecer el tiempo de disparo para que pueda disparar nuevamente cuando el personaje est� a la izquierda
            tiempo = 2f;
        }

        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 2 && player_pos.position.x < this.transform.position.x && player_pos.position.y < -1 && !estaMuerto)
        {
            SoundManager.instance.PlaySound("sonidoLanzar"); // Añade esta línea
            Instantiate(bala, puntoInstancia.position, Quaternion.identity);
            tiempo = 0;
        }

        if (atacado == 1) {
            estaMuerto = true;
            if (!haRotado90Grados)
            {
                // Rotar gradualmente hacia la derecha 90 grados
                Quaternion desiredRotation = Quaternion.Euler(0, 0, -90);

                // Interpolaci�n suave de la rotaci�n
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, speed * Time.deltaTime);

                // Verificar si ha rotado 90 grados
                if (Quaternion.Angle(transform.rotation, desiredRotation) < 0.1f)
                {
                    transform.rotation = desiredRotation; // Ajusta la rotaci�n exactamente a 90 grados
                    haRotado90Grados = true;
                this.enabled = false;
                }
            }
    }
    }
}
