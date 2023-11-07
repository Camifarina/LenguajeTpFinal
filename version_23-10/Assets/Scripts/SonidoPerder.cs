using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPerder : MonoBehaviour
{
    //private SoundManager sonido;
    public GameObject risaMalo;
    // Start is called before the first frame update
    void Start()
    {
       //sonido = GetComponent<AudioSource>();
       Instantiate(risaMalo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
