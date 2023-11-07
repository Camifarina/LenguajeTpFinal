using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flor : MonoBehaviour
{
    public Transform player_pos;
    public PlayerController raulcito;
    public bool agarrarFlor = false;
    public bool florDesactivada = false;
    public bool florInactiva = false;

    private BoxCollider2D florCollider;
    private Rigidbody2D rb;
    private Animator AnimatorFlor;
    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
        raulcito = player_pos.GetComponent<PlayerController>();

        florCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        AnimatorFlor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agarrarFlor == true)
        {
            AnimatorFlor.SetBool("florMuerta", true);
        }
        
        if (florInactiva == true)
        {
            AnimatorFlor.SetBool("florDesactiva", true);
        } else
        {
            AnimatorFlor.SetBool("florDesactiva", false);
        }

        if (florDesactivada == true)
        {
            florCollider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 0f;
        }
        Debug.Log("flor: " + agarrarFlor);
    }
}
