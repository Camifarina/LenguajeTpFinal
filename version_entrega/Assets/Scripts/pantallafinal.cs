using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pantallafinal : MonoBehaviour
{
    public Animator animador;

    void Start()
    {
        animador = GetComponent<Animator>();
        // Suscribir la función a llamar cuando la animación termine
        AnimationEvent evento = new AnimationEvent();
        evento.time = animador.GetCurrentAnimatorClipInfo(0)[0].clip.length; // Tiempo de duración de la animación
        evento.functionName = "CambiarDePantalla";
        animador.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].AddEvent(evento);
    }

    // Esta función será llamada cuando termine la animación
    void CambiarDePantalla()
    {
        SceneManager.LoadScene(6);
    }
}