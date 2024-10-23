/*using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�. Cambia seg�n tu implementaci�n.

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        LoadCurrentDay();
        Debug.Log("Se actualiza prefs: " + currentDay);
        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    // Este m�todo se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cu�ndo el personaje toca la cama.
            Debug.Log("El personaje est� tocando la cama");

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();

            // Mueve el personaje a la posici�n de la cama, al iniciar el siguiente d�a.
            // MoveCharacterToBed();
        }
    }

    // Este m�todo se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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

    // M�todo para cambiar el d�a actual, puedes llamarlo desde tu sistema de d�as.
    public void SetCurrentDay(int day)
    {
        Debug.Log("El valor del dia es: " + day);
        currentDay = day;
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            Debug.Log("El valor del dia es: " + currentDay);
        }
        else
        {
            Debug.Log("El valor del dia es: " + currentDay);
        }
    }
}*/
/*
using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�. Cambia seg�n tu implementaci�n.

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        LoadCurrentDay();
        Debug.Log("Se actualiza prefs: " + currentDay);
        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    // Este m�todo se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cu�ndo el personaje toca la cama.
            Debug.Log("El personaje est� tocando la cama");

            // Aumentar el d�a en 1 y guardar el nuevo valor en PlayerPrefs.
            IncrementDay();

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();

            // Mueve el personaje a la posici�n de la cama, al iniciar el siguiente d�a.
            //MoveCharacterToBed();
        }
    }

    // Este m�todo se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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

    // M�todo para incrementar el d�a actual y guardarlo.
    private void IncrementDay()
    {
        currentDay++; // Incrementar el d�a
        PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el d�a actualizado
        PlayerPrefs.Save(); // Aseg�rate de guardar los cambios
        Debug.Log("D�a incrementado a: " + currentDay); // Muestra el nuevo d�a en consola
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            Debug.Log("El valor del d�a es: " + currentDay);
        }
        else
        {
            Debug.Log("Valor por defecto del d�a: " + currentDay);
        }
    }
}*/

/*using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�. Cambia seg�n tu implementaci�n.
    private float gameTimeInMinutes; // Tiempo del juego en minutos
    private bool isTimeStopped = false; // Indica si el tiempo est� detenido
    [SerializeField] private TextMeshProUGUI timeText;

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        LoadCurrentDay();
        LoadGameTime();
        Debug.Log("Se actualiza prefs: " + currentDay);
        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    // Este m�todo se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cu�ndo el personaje toca la cama.
            Debug.Log("El personaje est� tocando la cama");

            // Aumentar el d�a en 1 y guardar el nuevo valor en PlayerPrefs.
            IncrementDay();

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();

            // Mueve el personaje a la posici�n de la cama.
            // MoveCharacterToBed();
        }
    }

    // Este m�todo se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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
        isTimeStopped = true; // Detiene el tiempo

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
        isTimeStopped = false; // Reanuda el tiempo
    }

    private void IncrementDay()
    {
        currentDay++; // Incrementar el d�a
        PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el d�a actualizado

        // Reiniciar la hora a las 5:00 AM (300 minutos)
        gameTimeInMinutes = 300f; // 5:00 AM en minutos
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actualizado

        PlayerPrefs.Save(); // Aseg�rate de guardar los cambios una sola vez
        Debug.Log("D�a incrementado a: " + currentDay); // Muestra el nuevo d�a en consola
    }

    private void UpdateTimeAndDayText()
    {
        // Calcular la hora actual en formato 24h a partir del tiempo en minutos
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un d�a
        int displayHour = totalMinutes / 60;
        int displayMinute = totalMinutes % 60;

        // Determinar AM o PM
        string period = displayHour >= 12 ? "PM" : "AM";
        displayHour = displayHour > 12 ? displayHour - 12 : displayHour; // Convertir a formato 12 horas
        displayHour = displayHour == 0 ? 12 : displayHour; // Asegurarse de que 0 sea 12

        // Mostrar el tiempo y el d�a actual
        timeText.text = $"D�a {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            Debug.Log("El valor del d�a es: " + currentDay);
        }
        else
        {
            Debug.Log("Valor por defecto del d�a: " + currentDay);
        }
    }

    private void LoadGameTime()
    {
        if (PlayerPrefs.HasKey("GameTimeInMinutes"))
        {
            gameTimeInMinutes = PlayerPrefs.GetFloat("GameTimeInMinutes");
            Debug.Log("El tiempo de juego actual es: " + gameTimeInMinutes);
        }
        else
        {
            gameTimeInMinutes = 300f; // Asigna 5:00 AM como valor por defecto si no existe en PlayerPrefs
            Debug.Log("Valor por defecto del tiempo de juego: " + gameTimeInMinutes);
        }
    }

    private void Update()
    {
        if (!isTimeStopped)
        {
            // Aumentar el tiempo en funci�n del tiempo real
            gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

            // Si se llega a 11:59 PM, incrementar el d�a
            if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
            {
                IncrementDay();
                gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
            }

            // Guardar el tiempo actual en PlayerPrefs
            PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
            PlayerPrefs.Save(); // Aseg�rate de guardar los cambios
        }
    }
}*/

using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el d�a.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el d�a actual est� almacenado aqu�.

    // Define la posici�n de la casilla a la que se mover� el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        // Cargar el d�a actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se carg� el d�a desde CycleDayController: " + currentDay);

        // Aseg�rate de que el panel (y el texto) est� completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    private void Update()
    {
        // No es necesario actualizar `CycleDayController.currentDay` en cada frame
    }

    // Este m�todo se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cu�ndo el personaje toca la cama.
            Debug.Log("El personaje est� tocando la cama");

            // Incrementa el d�a actual
            currentDay++;
            // Actualiza el d�a en CycleDayController
            CycleDayHouseController.currentDay = currentDay;
            // Reinicia el tiempo del juego
            CycleDayHouseController.gameTimeInMinutes = 300f;

            // Guarda el d�a actual en PlayerPrefs (opcional)
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            PlayerPrefs.Save(); // Aseg�rate de guardar los cambios

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();
        }
    }

    // Este m�todo se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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