using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_suelo : MonoBehaviour
{
    public Transform player_pos;
    public PlayerController raulcito;

    private BoxCollider2D mascaraCollider;
    private Rigidbody2D rb;

    public bool toco_mascara = false;


    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
        raulcito = player_pos.GetComponent<PlayerController>();

        mascaraCollider = GetComponent<BoxCollider2D>(); //Collider del villano
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(tiempo);
        if (toco_mascara)
        {
            mascaraCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f; // Desactivar la gravedad en 2D
        }
    }
}
