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
        //movimientos
        #region 
        if (Vector2.Distance(transform.position, player_pos.position) > distanciaFrenado)
        {
        transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player_pos.position) < distanciaRetraso)
        {
        transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player_pos.position) < distanciaFrenado && Vector2.Distance(transform.position, player_pos.position) > distanciaRetraso)
        {
        transform.position = transform.position;
        }
        #endregion
        //filp
        #region 
        if(player_pos.position.x>this.transform.position.x)
        {
            this.transform.localScale = new Vector2 (-2, 2);
        }else 
        {
            this.transform.localScale = new Vector2 (2, 2);
        }
        #endregion
        // disparo
        #region 
        tiempo += Time.deltaTime;
        if(tiempo >= 2)
        {
            Instantiate (bala, puntoInstancia.position, Quaternion.identity);
            tiempo = 0;
        }
        #endregion

    }
}
