
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Instancia única del SoundManager.

    public AudioSource soundSource; // Referencia al AudioSource principal para reproducir sonidos.

    public AudioClip[] soundClips; // Matriz para almacenar tus clips de sonido.


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Inicializa el AudioSource.
        soundSource = GetComponent<AudioSource>();

    }

    // Método para reproducir un sonido por nombre.
    public void PlaySound(string soundName)
    {
        // Busca el AudioClip por nombre.
        AudioClip sound = FindSoundByName(soundName);

        // Si se encuentra el sonido, reprodúcelo.
        if (sound != null)
        {
            soundSource.PlayOneShot(sound);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }

        
    }

    // Método para buscar un sonido por nombre.
    private AudioClip FindSoundByName(string soundName)
    {
        foreach (AudioClip clip in soundClips)
        {
            if (clip.name == soundName)
            {
                return clip;
            }
        }
        return null; // Devuelve null si no se encuentra el sonido.
    }

    public void PlayBackgroundMusic(string musicName)
{
    AudioClip music = FindSoundByName(musicName);

    if (music != null)
    {
        // Detén la música actual antes de reproducir la nueva.
        soundSource.Stop();
        soundSource.clip = music;
        soundSource.loop = true; // Establece la música en bucle.
        soundSource.Play();
    }
    else
    {
        Debug.LogWarning("Music not found: " + musicName);
    }
}
}

