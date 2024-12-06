
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public Transform player;                 // Referencia al jugador
    public AudioSource audioSource;          // El componente AudioSource que reproduce el sonido
    public GameObject marcoObject;           // El objeto que contiene el marco (o el sprite del marco)
    public AudioClip wolfMusic;              // M�sica del lobo
    public AudioClip ambientSound;           // Sonido ambiental 1
    public AudioClip bruma;                  // Sonido ambiental 2
    public float activationRadius = 50f;     // Radio de activaci�n en unidades (6 tiles de 0.64 x 0.64)

    private bool isPlayingAmbientSound1 = true;  // Indica qu� clip ambiental est� sonando
    private bool isPlayingWolfMusic = false;     // Indica si la m�sica del lobo est� sonando

    private void Start()
    {
        // Inicia la reproducci�n con el primer sonido ambiental
        PlayAmbientSound();
    }

    private void Update()
    {
        // Calcular la distancia entre el jugador y la fuente de sonido
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verificar si el objeto marco est� activado
        if (marcoObject != null && marcoObject.activeSelf)
        {
            // Reproducir m�sica del lobo si no est� ya reproduci�ndose
            if (!isPlayingWolfMusic)
            {
                StartWolfMusic();
            }
        }
        else
        {
            // Detener la m�sica del lobo y reproducir los sonidos ambientales si el marco no est� activo
            if (isPlayingWolfMusic)
            {
                StopWolfMusic();
                PlayAmbientSound(); // Cambiar al sonido ambiental inmediatamente
            }

            // Alternar entre ambientSound y bruma si el clip actual termin�
            if (!audioSource.isPlaying && !isPlayingWolfMusic)
            {
                PlayAmbientSound();
            }
        }

        // Controlar si el sonido debe reproducirse seg�n la distancia al jugador
        if (distanceToPlayer > activationRadius && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void StartWolfMusic()
    {
        isPlayingWolfMusic = true;
        audioSource.clip = wolfMusic;
        audioSource.loop = true;  // Repetir la m�sica del lobo
        audioSource.Play();
    }

    private void StopWolfMusic()
    {
        isPlayingWolfMusic = false;
        audioSource.Stop(); // Detener la m�sica del lobo
    }

    private void PlayAmbientSound()
    {
        // Alternar entre los dos sonidos ambientales
        if (isPlayingAmbientSound1)
        {
            audioSource.clip = ambientSound; // Cambiar al sonido ambiental 1
        }
        else
        {
            audioSource.clip = bruma; // Cambiar al sonido ambiental 2
        }

        isPlayingAmbientSound1 = !isPlayingAmbientSound1; // Alternar el estado
        audioSource.loop = false; // No repetir, queremos alternar al terminar
        audioSource.Play(); // Reproducir el nuevo clip
    }
}

/*
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class AmbientSound : MonoBehaviour
{
    public Transform player;                // Referencia al jugador
    public AudioSource audioSource;         // El componente AudioSource que reproduce el sonido
    public GameObject marcoObject;          // El objeto que contiene el marco (o el sprite del marco)
    public AudioClip wolfMusic;             // M�sica del lobo
    public AudioClip ambientSound;          // Sonido ambiental 1
    public AudioClip bruma;                 // Sonido ambiental 2
    public float activationRadius = 50f;    // Radio de activaci�n en unidades (6 tiles de 0.64 x 0.64)

    private bool isPlayingAmbientSound1 = true;  // Indica qu� clip ambiental est� sonando
    private float fadeDuration = 2f;             // Duraci�n del fade (en segundos)
    private float fadeSpeed;                     // Velocidad de la transici�n de volumen

    private void Start()
    {
        // Inicia la reproducci�n con el primer sonido ambiental
        PlayAmbientSound();
    }

    private void Update()
    {
        // Calcular la distancia entre el jugador y la fuente de sonido
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verificar si el objeto marco est� activado
        if (marcoObject != null && marcoObject.activeSelf)
        {
            // Reproducir m�sica del lobo si no est� ya reproduci�ndose
            if (audioSource.clip != wolfMusic)
            {
                // Ejecutar fade out e iniciar fade in con la m�sica del lobo
                StartCoroutine(FadeOutIn(wolfMusic));
            }
        }
        else
        {
            // Si el lobo no est� activo, alternar entre ambientSound y bruma
            if (!audioSource.isPlaying) // Verificar si el clip actual termin�
            {
                StartCoroutine(FadeOutIn(isPlayingAmbientSound1 ? ambientSound : bruma));
            }
        }

        // Controlar si el sonido debe reproducirse seg�n la distancia al jugador
        if (distanceToPlayer > activationRadius && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PlayAmbientSound()
    {
        // Alternar entre los dos sonidos ambientales
        if (isPlayingAmbientSound1)
        {
            audioSource.clip = ambientSound; // Cambiar al sonido ambiental 1
        }
        else
        {
            audioSource.clip = bruma; // Cambiar al sonido ambiental 2
        }

        isPlayingAmbientSound1 = !isPlayingAmbientSound1; // Alternar el estado
        audioSource.loop = false; // No repetir, queremos alternar al terminar
        audioSource.Play(); // Reproducir el nuevo clip
    }

    private IEnumerator FadeOutIn(AudioClip newClip)
    {
        // Realizar fade out (desvanecimiento del sonido actual)
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Realizar fade in (aumento gradual del volumen del nuevo sonido)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;  // Asegurarse de que el volumen final sea el original
    }
}
*/