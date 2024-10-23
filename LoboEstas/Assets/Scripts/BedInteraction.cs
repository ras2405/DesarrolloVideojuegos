
/*using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el día.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el día actual esté almacenado aquí. Cambia según tu implementación.

    // Define la posición de la casilla a la que se moverá el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        LoadCurrentDay();
        Debug.Log("Se actualiza prefs: " + currentDay);
        // Asegúrate de que el panel (y el texto) esté completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    private void Update()
    {
        CycleDayController.currentDay = currentDay;
    }

    // Este método se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cuándo el personaje toca la cama.
            Debug.Log("El personaje está tocando la cama");

            currentDay++;
            CycleDayController.gameTimeInMinutes = 300f;
            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();

            // Mueve el personaje a la posición de la cama, al iniciar el siguiente día.
            // MoveCharacterToBed();
        }
    }

    // Este método se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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

    // Esta función muestra el día actual en la pantalla y activa el panel.
    private void ShowDayOnScreen()
    {
        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        UpdateDayText();

        // Desactiva el panel automáticamente después de 5 segundos.
        Invoke("HideDayPanel", 5f);
    }

    // Esta función actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "Día: " + currentDay.ToString(); // Cambia el formato si lo necesitas.
    }

    // Método para ocultar el panel que recubre el texto.
    private void HideDayPanel()
    {
        textPanel.SetActive(false); // Desactiva el panel y el texto.
    }

      private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
    }
}*/

/*using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el día.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el día actual esté almacenado aquí.

    // Define la posición de la casilla a la que se moverá el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        // Cargar el día actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se cargó el día desde CycleDayController: " + currentDay);

        // Asegúrate de que el panel (y el texto) esté completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    private void Update()
    {
        // No es necesario actualizar `CycleDayController.currentDay` en cada frame
    }

    // Este método se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cuándo el personaje toca la cama.
            Debug.Log("El personaje está tocando la cama");

            // Incrementa el día actual
            currentDay++;
            // Actualiza el día en CycleDayController
            CycleDayHouseController.currentDay = currentDay;
            // Reinicia el tiempo del juego
            CycleDayHouseController.gameTimeInMinutes = 300f;

            // Guarda el día actual en PlayerPrefs (opcional)
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            PlayerPrefs.Save(); // Asegúrate de guardar los cambios

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();
        }
    }

    // Este método se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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

    // Esta función muestra el día actual en la pantalla y activa el panel.
    private void ShowDayOnScreen()
    {
        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        UpdateDayText();

        // Desactiva el panel automáticamente después de 5 segundos.
        Invoke("HideDayPanel", 5f);
    }

    // Esta función actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "Día: " + currentDay.ToString(); // Cambia el formato si lo necesitas.
    }

    // Método para ocultar el panel que recubre el texto.
    private void HideDayPanel()
    {
        textPanel.SetActive(false); // Desactiva el panel y el texto.
    }

    // Método para cargar el día actual desde CycleDayController
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("Día cargado desde CycleDayController: " + currentDay);
    }
}*/

using UnityEngine;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel; // El panel que recubre el texto, que contiene tanto el texto como el fondo.
    public TextMeshProUGUI dayText; // El texto en la UI para mostrar el día.
    public GameObject character; // El objeto del personaje que se va a mover.
    private int currentDay = 1; // Suponiendo que el día actual esté almacenado aquí.

    // Define la posición de la casilla a la que se moverá el personaje al tocar la cama.
    public Vector2 bedPosition = new Vector2(0, -2);

    private void Start()
    {
        // Cargar el día actual desde el CycleDayController
        LoadCurrentDay();
        Debug.Log("Se cargó el día desde CycleDayController: " + currentDay);

        // Asegúrate de que el panel (y el texto) esté completamente desactivado al iniciar.
        textPanel.SetActive(false);
    }

    private void Update()
    {
        // No es necesario actualizar `CycleDayController.currentDay` en cada frame
    }

    // Este método se ejecuta cuando el personaje entra en contacto con la cama (usando Trigger).
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            // Agrega un log para saber cuándo el personaje toca la cama.
            Debug.Log("El personaje está tocando la cama");

            // Incrementa el día actual
            currentDay++;
            // Actualiza el día en CycleDayController
            CycleDayHouseController.currentDay = currentDay;
            // Reinicia el tiempo del juego
            CycleDayHouseController.gameTimeInMinutes = 300f;

            // Guarda el día actual en PlayerPrefs (opcional)
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            PlayerPrefs.Save(); // Asegúrate de guardar los cambios

            // Muestra el panel y el texto cuando el personaje toca la cama.
            ShowDayOnScreen();
        }
    }

    // Este método se ejecuta cuando el personaje deja de tocar la cama.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
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

    // Esta función muestra el día actual en la pantalla y activa el panel.
    private void ShowDayOnScreen()
    {
        textPanel.SetActive(true); // Activa el panel que recubre el texto.
        UpdateDayText();

        // Desactiva el panel automáticamente después de 5 segundos.
        Invoke("HideDayPanel", 5f);
    }

    // Esta función actualiza el texto en la UI.
    private void UpdateDayText()
    {
        dayText.text = "Día: " + currentDay.ToString(); // Cambia el formato si lo necesitas.
    }

    // Método para ocultar el panel que recubre el texto.
    private void HideDayPanel()
    {
        textPanel.SetActive(false); // Desactiva el panel y el texto.
    }

    // Método para cargar el día actual desde CycleDayController
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("Día cargado desde CycleDayController: " + currentDay);
    }
}