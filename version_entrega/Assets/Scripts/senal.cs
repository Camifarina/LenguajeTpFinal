using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class senal : MonoBehaviour
{
    private Transform raul;
    private PlayerController raulcito;
    private Transform Reiniciar;
    private Reinicio reinicio;
    private Transform iniciar;
    private Boton boton;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
        raul = GameObject.Find("Player").transform;
        raulcito = raul.GetComponent<PlayerController>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4) 
        {
        Reiniciar = GameObject.Find("Reiniciar").transform;
        reinicio = Reiniciar.GetComponent<Reinicio>();
        }
        if(SceneManager.GetActiveScene().buildIndex == 0) {
        iniciar = GameObject.Find("Iniciar").transform;
        boton = iniciar.GetComponent<Boton>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            Destroy(gameObject);
    }
}
