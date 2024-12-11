using UnityEngine;
using TMPro;
using System.Collections;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.

    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI notNightText;

    public GameObject cutsceneBG;

    private CycleDayController cycleDayController;

    public KeyCode interactionKey = KeyCode.E;
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�.

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(-25, -1);

    private bool isNight = false;
    private bool isNearBed = false;

    public AudioSource audioSource; // Componente para reproducir los sonidos.
    public AudioClip[] dayTransitionClips; // Arreglo de clips de audio para cada d�a.
    public AudioSource houseAmbient;

    private HouseInteraction houseInteraction;


    private void Start()
    {
        GameObject houseObject = GameObject.Find("house");
        if (houseObject != null)
        {
            houseInteraction = houseObject.GetComponent<HouseInteraction>();
        }

        if (houseInteraction == null)
        {
            Debug.LogError("No se encontró HouseInteraction en el objeto 'HouseObject'.");
        }
        // Cargar el d�a actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se carg� el día desde CycleDayController: " + currentDay);

        cycleDayController = FindObjectOfType<CycleDayController>();

        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
        textPanel.SetActive(false);
        cutsceneBG.SetActive(false);
        interactionText.gameObject.SetActive(false);
        notNightText.gameObject.SetActive(false);
    }

    private void Update()
    {
        isNight = cycleDayController.IsNight();
        if (isNearBed)// && cycleDayController.IsNight())
        {
            notNightText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(true); // Mostramos el texto
            if (Input.GetKeyDown(interactionKey)) // Si el jugador presiona la tecla de interacci�n
            {
                Debug.Log("Se ejecuta Dormir en BedInteraction");
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
            interactionText.gameObject.SetActive(false); // Ocultamos el texto si no est� cerca
        }
    }

    // Este m�todo se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = true;
            Debug.Log("El personaje est� tocando la cama");
        }
    }

    private void Dormir()
    {
        // Incrementa el d�a actual
        currentDay++;
        // Actualiza el d�a en CycleDayController
        CycleDayController.currentDay = currentDay; // CycleDayHouseController
                                                    // Reinicia el tiempo del juego
        CycleDayController.gameTimeInMinutes = 300f;

        // Guarda el d�a actual en PlayerPrefs (opcional)
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save(); // Aseg�rate de guardar los cambios

        // Muestra el panel y el texto cuando el personaje toca la cama.
        //ShowDayOnScreen();


        Debug.Log("Se ejecuta WaitForSecondsExample()");
        //ShowClueOnScreen(currentDay - 1);
        cutsceneBG.SetActive(true);
        StartCoroutine(WaitForSecondsToClue());
    }

    // Este m�todo se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = false;
            // Agrega un log para saber cu�ndo el personaje deja de tocar la cama.
            Debug.Log("El personaje dej� de tocar la cama");

            // Oculta el panel y el texto cuando el personaje deja de tocar la cama.
            HideDayPanel();
        }
    }

    IEnumerator WaitForSecondsToClue()
    {
        yield return new WaitForSeconds((float)houseInteraction.VideoLength() - 4); // Espera que termine el video
        //StartCoroutine(WaitForVideoAndShowClue());
        Debug.Log("Video finalizado, ejecuta ShowClueOnScreen");
        ShowClueOnScreen(currentDay - 1);
    }

    // Funci�n para mover el personaje a la casilla donde est� la cama.
    private void MoveCharacterToBed()
    {
        character.transform.position = bedPosition; // Mueve el personaje a la posici�n especificada.
    }

    private void ShowClueOnScreen(int num)
    {
        string clue = "";
        if (num == 1)
        {
            clue = " \n Te voy a romper los cultivos....";
        }
        else if (num == 2)
        {
            clue = " \n Voy a romper tu puerta.... ";
        }
        else if (num == 3)
        {
            clue = " \n Voy a romperte la ventana! ";
        }
        else if (num == 4)
        {
            clue = " \n Voy a meterme por tu chimenea! ";
        }

        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        dayText.text = "Día: " + currentDay.ToString() + clue;
        Debug.Log("Se activa el panel con Día: " + currentDay.ToString() + clue);
        // Inicia la corrutina para esperar a que termine el audio antes de ocultar el panel.
        Debug.Log("Ejecuta  StartCoroutine(WaitForAudioAndHidePanel(currentDay))");
        StartCoroutine(WaitForAudioAndHidePanel(currentDay));
    }

    // Corrutina para esperar a que termine el audio antes de ocultar el panel.
    private IEnumerator WaitForAudioAndHidePanel(int day)
    {
        Debug.Log("WaitForAudioAndHidePanel, ejecuta el audio del mensaje:  PlayDayTransitionAudio(day)");
        yield return new WaitForSeconds(3f); // Espera que termine el video
                                             //StartCoroutine(WaitForVideoAndShowClue());
        PlayDayTransitionAudio(day); // Reproduce el audio.

        // Espera a que el audio termine.
        while (audioSource.isPlaying)
        {
            yield return null; // Espera un frame antes de volver a comprobar.
        }

        // Una vez que el audio haya terminado, oculta el panel despu�s de un breve retraso (opcional).
        yield return new WaitForSeconds(1f); // Opcional: a�ade un retraso adicional.
        HideDayPanel();
    }

    // Esta funci�n actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "D�a: " + currentDay.ToString();
    }

    private void HideDayPanel()
    {
        cutsceneBG.SetActive(false);
        textPanel.SetActive(false); // Desactiva el panel y el texto.

        // Detener la reproducci�n del audio si est� sonando.
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio detenido al ocultar el panel.");
        }
    }

    // M�todo para cargar el d�a actual desde CycleDayController
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("D�a cargado desde CycleDayController: " + currentDay);
    }

    private void PlayDayTransitionAudio(int day)
    {
        Debug.Log("Se llama a PlayDayTransitionAudio(int day)");
        int clipIndex = day - 2;

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
                Debug.Log($"Reproduciendo audio de transici�n para el d�a {day}");

                // Inicia una corrutina para reactivar "houseAmbient" cuando termine
                StartCoroutine(ResumeHouseAmbientAfterAudio());
            }
        }
        else
        {
            Debug.LogWarning($"No se encontr� un clip para el d�a {day} o AudioSource no asignado");
        }
    }

    private IEnumerator ResumeHouseAmbientAfterAudio()
    {
        // Espera a que el audio de transici�n termine
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