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
    /*[SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora
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
        currentDay = CycleDayController.currentDay;
   
    }

    private void LoadGameTime()
    {
        gameTimeInMinutes = CycleDayController.gameTimeInMinutes;
      
    }*/


    //[SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    public static int currentDay = 1;
    public static float gameTimeInMinutes = 300f;


    //private int currentDay = 1; // Contador para el día actual
    // private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Start()
    {
        currentDay = CycleDayController.currentDay;
        gameTimeInMinutes = CycleDayController.gameTimeInMinutes;

        timePerCycle = 120f; // Establecer el tiempo para el ciclo Day (2 minutos)
        UpdateTimeAndDayText(); // Actualiza el texto al iniciar
    }

    private void Update()
    {
        actualTimeCycle += Time.deltaTime;
        averageCycle = actualTimeCycle / timePerCycle;

        // Si se completa un ciclo
        if (actualTimeCycle >= timePerCycle)
        {
            actualTimeCycle = 0; // Comenzar nuevo ciclo
            actualCycle = nextCycle;

            // Cambiar al siguiente ciclo
            nextCycle = (nextCycle + 1) % cyclesDay.Length; // Cambiar al siguiente ciclo

            // Ajustar timePerCycle para ciclos restantes
            if (nextCycle == 0) // Vuelve a Day
            {
                timePerCycle = 120f; // 120 segundos para el ciclo Day
            }
            else
            {
                timePerCycle = 45f; // 45 segundos para los ciclos restantes
            }

            UpdateTimeAndDayText(); // Actualizar el texto del día
        }

        // Incrementar el tiempo del juego en minutos, basado en el tiempo transcurrido
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
        {
            currentDay++;
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios

        // Cambiar color
        //ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();

        /* // Comprobar si es 11:59 PM para incrementar el día
         if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
         {
             currentDay++;
             gameTimeInMinutes = 0; // Reiniciar el tiempo del juego

             // Guardar el día actual en PlayerPrefs
             SaveCurrentDay();
         }*/

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
       
        CycleDayController.currentDay = currentDay;
        CycleDayController.gameTimeInMinutes = gameTimeInMinutes;

    }

    // Método para guardar el día actual en PlayerPrefs
    private void SaveCurrentDay()
    {
        Debug.Log("Se actualiza prefs: " + currentDay);
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios
    }

    // Método para cargar el día actual desde PlayerPrefs
    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        /*if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
        }*/
    }
}