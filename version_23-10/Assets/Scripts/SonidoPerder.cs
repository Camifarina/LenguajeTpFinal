using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPerder : MonoBehaviour
{
    private SoundManager sonido;
    private AudioSource audioP;
    // Start is called before the first frame update
    void Start()
    {
        audioP = GetComponent<AudioSource>();
        SoundManager.instance.PlayBackgroundMusic("cuernoPerdiste");
    }

    // Update is called once per frame
    void Update()
    {
        if (sonido.hayMusicaDeFondo == false)
        {
            sonido.backgroundMusicSource.Play();
        }
    }
}
