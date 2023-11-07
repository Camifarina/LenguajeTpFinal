using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador que la c�mara debe seguir.
    public float smoothSpeed = 5f; // Velocidad de suavizado del seguimiento.

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x-2.5f, transform.position.y, transform.position.z);
            // Solo ajustamos la posici�n en el eje X, manteniendo la posici�n en el eje Y y Z.

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}