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
        if (raulcito.mareado == false)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                Destroy(gameObject);
                time = 0;
            }
        }
        if (raulcito.eSinMascara == false)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                Destroy(gameObject);
                time = 0;
            }
        }
        if (raulcito.eMuerto == false)
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
