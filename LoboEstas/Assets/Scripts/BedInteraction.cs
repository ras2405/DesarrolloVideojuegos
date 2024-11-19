using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.

    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI notNightText;

    private CycleDayController cycleDayController;

    public KeyCode interactionKey = KeyCode.E;
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�.

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(-25, -1);

    private bool isNight = false;
    private bool isNearBed = false;

    private void Start()
    {
        // Cargar el d�a actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se carg� el d�a desde CycleDayController: " + currentDay);

        cycleDayController = FindObjectOfType<CycleDayController>();

        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
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
            if (Input.GetKeyDown(interactionKey)) // Si el jugador presiona la tecla de interacci�n
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
        ShowDayOnScreen();
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

    // Funci�n para mover el personaje a la casilla donde est� la cama.
    private void MoveCharacterToBed()
    {
        character.transform.position = bedPosition; // Mueve el personaje a la posici�n especificada.
    }

    // Esta funci�n muestra el d�a actual en la pantalla y activa el panel.
    private void ShowDayOnScreen()
    {
        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        UpdateDayText();

        // Desactiva el panel autom�ticamente despu�s de 5 segundos.
        Invoke("HideDayPanel", 5f);
    }

    // Esta funci�n actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "D�a: " + currentDay.ToString(); // Cambia el formato si lo necesitas.
    }

    // M�todo para ocultar el panel que recubre el texto.
    private void HideDayPanel()
    {
        textPanel.SetActive(false); // Desactiva el panel y el texto.
    }

    // M�todo para cargar el d�a actual desde CycleDayController
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("D�a cargado desde CycleDayController: " + currentDay);
    }
}