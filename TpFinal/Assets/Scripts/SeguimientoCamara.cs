using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador que la cámara debe seguir.
    public float smoothSpeed = 5f; // Velocidad de suavizado del seguimiento.

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            // Solo ajustamos la posición en el eje X, manteniendo la posición en el eje Y y Z.

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}