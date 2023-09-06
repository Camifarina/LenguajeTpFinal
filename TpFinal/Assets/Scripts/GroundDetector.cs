using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private bool isGrounded = false;

    // Método para comprobar si el personaje está en el suelo.
    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con el suelo, establece isGrounded en true.
        if (collision.CompareTag("Ground")) // Asegúrate de que el tag del suelo sea "Ground".
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si deja de colisionar con el suelo, establece isGrounded en false.
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
