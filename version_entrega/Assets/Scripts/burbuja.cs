using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burbuja : MonoBehaviour
{
    private Transform raul;
    private PlayerController raulcito;

    void Start()
    {
        raul = GameObject.Find("Player").transform;
        raulcito = raul.GetComponent<PlayerController>();
    }

    void Update()
    {
                Destroy(gameObject);
    }
}
