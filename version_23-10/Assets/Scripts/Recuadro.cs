using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recuadro : MonoBehaviour
{
    // Start 
    private Transform raul;
    private PlayerController raulcito;

    public float time;
    void Start()
    {
        raul = GameObject.Find("Player").transform;
        raulcito = raul.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raulcito.golpeado == false)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                Destroy(gameObject);
                time = 0;
            }
        }
        if (raulcito.liberoCiudadano == false)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                Destroy(gameObject);
                time = 0;
            }
        }
        if (raulcito.matoCiudadano == false)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                Destroy(gameObject);
                time = 0;
            }
        }
    }
}
