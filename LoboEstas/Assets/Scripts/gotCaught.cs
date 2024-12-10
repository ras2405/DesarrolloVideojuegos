using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gotCaught : MonoBehaviour
{
    // Referencia al DeadPanel y su Imagen
    public GameObject deadPanel;
    public SpriteRenderer deadImage; // La imagen que se va a difuminar

    public AudioSource deathSound; // Sonido que se va a difuminar (asignar en el Inspector)

    // Tiempo de duración para las transiciones
    public float fadeDuration = 4.0f; 

    // Referencia al botón que se debe mostrar al final
    public GameObject restartButton; 

    private bool isFading = false; // Bandera para evitar que se ejecute más de una vez

    void Start()
    {
        // Obtener la imagen del DeadPanel
        if (deadPanel != null && deadImage != null)
        {
            Color tempColor = deadImage.color;
            tempColor.a = 1f; // Asegura que inicie completamente opaco
            deadImage.color = tempColor;
        }

        // Asegurar que el botón esté oculto al inicio
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si el DeadPanel está activo y no hemos empezado a difuminar
        if (deadPanel.activeSelf && !isFading)
        {
            isFading = true; // Evitar llamadas repetidas
            StartCoroutine(FadeOutEffects());
        }
    }

    // Corrutina para manejar las transiciones
    IEnumerator FadeOutEffects()
    {
        float timer = 0f;

        // Guarda el volumen inicial del sonido
        float startVolume = deathSound != null ? deathSound.volume : 0f;


        if (deathSound != null && deathSound.clip != null)
        {
            if (deathSound.clip.length > 5f) 
            {
                deathSound.Stop();           
                deathSound.time = 5f;        
               
            }
            deathSound.Play();           
        }

            // Realiza el fade out del sonido y la imagen simultáneamente
            while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float fadeRatio = timer / fadeDuration;

            // Reducir el volumen del sonido
            if (deathSound != null)
            {
                deathSound.volume = Mathf.Lerp(startVolume, 0f, fadeRatio);
            }

            // Reducir la opacidad de la imagen del DeadPanel
            if (deadImage != null)
            {
                Color tempColor = deadImage.color;
                tempColor.a = Mathf.Lerp(1f, 0f, fadeRatio);
                deadImage.color = tempColor;
            }

            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que el volumen y la opacidad lleguen a 0
        if (deathSound != null)
        {
            deathSound.volume = 0f;
            deathSound.Stop();
        }

        if (deadImage != null)
        {
            Color tempColor = deadImage.color;
            tempColor.a = 0f;
            deadImage.color = tempColor;
        }

        // Mostrar el botón al final
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
    }
}
