/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayHouseController : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Referencia al texto en la UI donde mostrar el tiempo y el día.

    private void Start()
    {
        // Cargar el día actual
        int currentDay = PlayerPrefs.GetInt("CurrentDay", 0); // 1 es el valor por defecto si no se encuentra.

        // Cargar el tiempo actual
        float gameTimeInMinutes = PlayerPrefs.GetFloat("GameTimeInMinutes", 0f); // 0 es el valor por defecto si no se encuentra.

        // Convertir el tiempo a horas y minutos
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día
        int displayHour = totalMinutes / 60;
        int displayMinute = totalMinutes % 60;

        // Determinar AM o PM
        string period = displayHour >= 12 ? "PM" : "AM";
        displayHour = displayHour > 12 ? displayHour - 12 : displayHour; // Convertir a formato 12 horas
        displayHour = displayHour == 0 ? 12 : displayHour; // Asegurarse de que 0 sea 12

        // Mostrar el tiempo y el día actual
        timeText.text = $"Día {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
    }
}

*/

using UnityEngine;
using TMPro;

public class CycleDayHouseController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora
    private int currentDay; // Días que se mantienen sincronizados
    private float gameTimeInMinutes; // Tiempo del juego en minutos

    private void Start()
    {
        // Cargar el día y el tiempo desde PlayerPrefs
        LoadCurrentDay();
        LoadGameTime();

        // Inicializar el texto
        UpdateTimeAndDayText();
    }

    private void Update()
    {
        // Aumentar el tiempo en función del tiempo real
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        // Si se llega a 11:59 PM, incrementar el día
        if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
        {
            currentDay++;
            PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el día actual
            PlayerPrefs.Save(); // Asegúrate de guardar los cambios
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }

        // Actualizar el texto de hora
        UpdateTimeAndDayText();

        // Guardar el tiempo actual en PlayerPrefs
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios
    }

    private void UpdateTimeAndDayText()
    {
        // Calcular la hora actual en formato 24h a partir del tiempo en minutos
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día
        int displayHour = totalMinutes / 60;
        int displayMinute = totalMinutes % 60;

        // Determinar AM o PM
        string period = displayHour >= 12 ? "PM" : "AM";
        displayHour = displayHour > 12 ? displayHour - 12 : displayHour; // Convertir a formato 12 horas
        displayHour = displayHour == 0 ? 12 : displayHour; // Asegurarse de que 0 sea 12

        // Mostrar el tiempo y el día actual
        timeText.text = $"Día {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
        }
        else
        {
            currentDay = 1; // Asigna un valor por defecto si no existe en PlayerPrefs
        }
    }

    private void LoadGameTime()
    {
        if (PlayerPrefs.HasKey("GameTimeInMinutes"))
        {
            gameTimeInMinutes = PlayerPrefs.GetFloat("GameTimeInMinutes");
        }
        else
        {
            gameTimeInMinutes = 300f; // Asigna un valor por defecto si no existe en PlayerPrefs
        }
    }
}