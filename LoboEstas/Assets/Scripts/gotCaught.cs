using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gotCaught : MonoBehaviour
{
    public GameObject deadPanel;
    public SpriteRenderer deadImage; // La imagen que se va a difuminar

    public AudioSource deathSound; // Sonido que se va a difuminar (asignar en el Inspector)

    public float fadeDuration = 4.0f; 

    public GameObject restartButton; 

    private bool isFading = false;

    private HouseInteraction houseInteraction;
    private bool isVidPlaying = false;

    void Start()
    {
        deathSound.Stop();
        deathSound.gameObject.SetActive(false);
        deadImage.gameObject.SetActive(false);

        houseInteraction = FindObjectOfType<HouseInteraction>();
        isVidPlaying = houseInteraction.vidPlaying;

        if (!isVidPlaying)
        {
            deathSound.gameObject.SetActive(true);
            deadImage.gameObject.SetActive(true);

            if (deadPanel != null && deadImage != null)
            {
                Color tempColor = deadImage.color;
                tempColor.a = 1f; 
                deadImage.color = tempColor;
            }

        }
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    void Update()
    {
        if (deadPanel.activeSelf && !isFading)
        {
            isFading = true; 
            StartCoroutine(FadeOutEffects());
        }
    }

    IEnumerator FadeOutEffects()
    {
        float timer = 0f;

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

        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
    }
}
