using UnityEngine;
using TMPro;
using System.Collections;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el día.

    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI notNightText;

    private CycleDayController cycleDayController;

    public KeyCode interactionKey = KeyCode.E;
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el día actual esté almacenado aquí.

    // Define la posición de la casilla a la que se moverá el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(-25, -1);

    private bool isNight = false;
    private bool isNearBed = false;

    public AudioSource audioSource; // Componente para reproducir los sonidos.
    public AudioClip[] dayTransitionClips; // Arreglo de clips de audio para cada día.
    public AudioSource houseAmbient;

    private void Start()
    {
        // Cargar el día actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se cargó el día desde CycleDayController: " + currentDay);

        cycleDayController = FindObjectOfType<CycleDayController>();

        // Asegúrate de que el panel (y el texto) esté completamente desactivado al iniciar.
        textPanel.SetActive(false);
        interactionText.gameObject.SetActive(false);
        notNightText.gameObject.SetActive(false);
    }

    private void Update()
    {
        isNight = cycleDayController.IsNight();
        if (isNearBed && cycleDayController.IsNight())
        {
            notNightText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(true); // Mostramos el texto
            if (Input.GetKeyDown(interactionKey)) // Si el jugador presiona la tecla de interacción
            {
                Dormir();
            }
        }
        else if (isNearBed) 
        {
            notNightText.gameObject.SetActive(true);
            interactionText.gameObject.SetActive(false);
        }
        else
        {
            notNightText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(false); // Ocultamos el texto si no está cerca
        }
    }

    // Este método se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = true;
            Debug.Log("El personaje está tocando la cama");
        }
    }

    private void Dormir()
    {
        // Incrementa el día actual
        currentDay++;
        // Actualiza el día en CycleDayController
        CycleDayController.currentDay = currentDay; // CycleDayHouseController
                                                    // Reinicia el tiempo del juego
        CycleDayController.gameTimeInMinutes = 300f;

        // Guarda el día actual en PlayerPrefs (opcional)
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios

        // Muestra el panel y el texto cuando el personaje toca la cama.
        //ShowDayOnScreen();

        ShowClueOnScreen(currentDay - 1);

        // Dia 1 es de exploracion
      /*  if (currentDay == 2)
        {
            Debug.Log("Fin del dia 1: Mostrar transicion 1 (Lobo indica que rompera los cultivos)");
           
        }
        else if (currentDay == 3) {
            Debug.Log("Fin del dia 2: Si las vallas no estan mejoradas, cultivos desaparecen. Mostrar transicion 2 (Lobo indica que rompera la ventana)");
            
        }
        else if (currentDay == 4)
        {
            Debug.Log("Fin del dia 3: Si las vallas no estan mejoradas, cultivos desaparecen. Si ventana no reforzadas, Bad Ending. Sino mostrar transicion 3 (Lobo indica que rompera la puerta)");
        }
        else if (currentDay == 5)
        {
            Debug.Log("Fin del dia 4: Si las vallas no estan mejoradas, cultivos desaparecen. Si ventana y/o puerta no reforzada, Bad Ending. Sino mostrar transicion 4 (Lobo indica que ira a la chimenea)");
        }
        else if (currentDay == 6)
        {
            Debug.Log("Fin del dia 5: Si ventana y/o puerta no reforzada, Bad Ending. Si estufa apagada, Bad Ending. Sino mostrar Good Ending");
        }*/
    }

    // Este método se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = false;
            // Agrega un log para saber cuándo el personaje deja de tocar la cama.
            Debug.Log("El personaje dejó de tocar la cama");

            // Oculta el panel y el texto cuando el personaje deja de tocar la cama.
            HideDayPanel();
        }
    }

    // Función para mover el personaje a la casilla donde está la cama.
    private void MoveCharacterToBed()
    {
        character.transform.position = bedPosition; // Mueve el personaje a la posición especificada.
    }

    private void ShowClueOnScreen(int num)
    {
        string clue = "";
        if (num == 1)
        {
            clue = " \n LOBO: Aplastare tus cultivos ... \n Esto es solo el comienzo.";
        }
        else if (num == 2)
        {
            clue = " \n LOBO: Atravesare tu ventana ... ";
        }
        else if (num == 3)
        {
            clue = " \n LOBO: Atravesare tu puerta ... ";
        }
        else if (num == 4)
        {
            clue = " \n LOBO: Me meteré por tu chimenea ... \n ¡No podrás detenerme!";
        }

        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        dayText.text = "Día: " + currentDay.ToString() + clue;

        // Inicia la corrutina para esperar a que termine el audio antes de ocultar el panel.
        StartCoroutine(WaitForAudioAndHidePanel(currentDay));
    }

    // Corrutina para esperar a que termine el audio antes de ocultar el panel.
    private IEnumerator WaitForAudioAndHidePanel(int day)
    {
        PlayDayTransitionAudio(day); // Reproduce el audio.

        // Espera a que el audio termine.
        while (audioSource.isPlaying)
        {
            yield return null; // Espera un frame antes de volver a comprobar.
        }

        // Una vez que el audio haya terminado, oculta el panel después de un breve retraso (opcional).
        yield return new WaitForSeconds(1f); // Opcional: añade un retraso adicional.
        HideDayPanel();
    }

    // Esta función actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "Día: " + currentDay.ToString();
    }

    private void HideDayPanel()
    {
        textPanel.SetActive(false); // Desactiva el panel y el texto.

        // Detener la reproducción del audio si está sonando.
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio detenido al ocultar el panel.");
        }
    }

    // Método para cargar el día actual desde CycleDayController
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("Día cargado desde CycleDayController: " + currentDay);
    }

    private void PlayDayTransitionAudio(int day)
    {
        Debug.Log("Se llama a PlayDayTransitionAudio(int day)");
        int clipIndex = day;

        // Desactiva el GameObject de "houseAmbient" antes de reproducir el clip
        if (houseAmbient != null && houseAmbient.gameObject.activeSelf)
        {
            houseAmbient.gameObject.SetActive(false);
            Debug.Log("GameObject 'houseAmbient' desactivado.");
        }

        if (clipIndex >= 0 && clipIndex < dayTransitionClips.Length && audioSource != null)
        {
            AudioClip clip = dayTransitionClips[clipIndex];
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log($"Reproduciendo audio de transición para el día {day}");

                // Inicia una corrutina para reactivar "houseAmbient" cuando termine
                StartCoroutine(ResumeHouseAmbientAfterAudio());
            }
        }
        else
        {
            Debug.LogWarning($"No se encontró un clip para el día {day} o AudioSource no asignado");
        }
    }

    private IEnumerator ResumeHouseAmbientAfterAudio()
    {
        // Espera a que el audio de transición termine
        while (audioSource.isPlaying)
        {
            yield return null; // Espera un frame
        }

        // Reactiva el GameObject de "houseAmbient"
        if (houseAmbient != null && !houseAmbient.gameObject.activeSelf)
        {
            houseAmbient.gameObject.SetActive(true);
            Debug.Log("GameObject 'houseAmbient' reactivado.");
        }
    }

}