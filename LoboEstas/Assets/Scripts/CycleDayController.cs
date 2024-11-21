using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora
    [SerializeField] private GameObject[] clockHands; // Arreglo de GameObjects para los sprites de las agujas

    private int actualCycle = 0;
    private int nextCycle = 1;
    public static int currentDay = 1;
    public static float gameTimeInMinutes = 300f;

    private void Start()
    {
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        // Establecer el color de la luz según el ciclo inicial
        globalLight.color = cyclesDay[0].cycleColor;
        UpdateTimeAndDayText(); // Actualizar el texto al iniciar
        UpdateClockHands(0); // Inicia mostrando el sprite correspondiente a "Mañana"
    }

    private void Update()
    {
        // Incrementar el tiempo del juego en minutos
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        if (gameTimeInMinutes >= 1439) // Si es 11:59 PM en el reloj del juego
        {
            currentDay++;
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        // Actualizar la luz según el tiempo actual
        UpdateDayStageAndLight();

        // Actualizar el texto del tiempo y día
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        // Verificar el tiempo y ajustar la luz y los sprites de acuerdo con el ciclo
        if (totalMinutes >= 300 && totalMinutes < 480) // 5:00 AM - 7:59 AM
        {
            SetDayStage(0); // Mañana
            UpdateClockHands(0); // Activar AgujaMorning
        }
        else if (totalMinutes >= 480 && totalMinutes < 960) // 8:00 AM - 3:59 PM
        {
            SetDayStage(1); // Día
            UpdateClockHands(1); // Activar AgujaDay
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080) // 4:00 PM - 5:59 PM
        {
            SetDayStage(2); // Tarde
            UpdateClockHands(2); // Activar AgujaEvening
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260) // 6:00 PM - 8:59 PM
        {
            SetDayStage(3); // Atardecer
            UpdateClockHands(3); // Activar AgujaNight
        }
        else // 9:00 PM - 4:59 AM
        {
            SetDayStage(4); // Noche
            UpdateClockHands(4); // Activar AgujaLateNight
        }
    }

    private void UpdateClockHands(int activeIndex)
    {
        for (int i = 0; i < clockHands.Length; i++)
        {
            clockHands[i].SetActive(i == activeIndex); // Activa solo el sprite correspondiente
        }
    }

    private void SetDayStage(int stageIndex)
    {
        if (actualCycle != stageIndex) // Solo cambiar si el ciclo es diferente
        {
            actualCycle = stageIndex;
            nextCycle = (stageIndex + 1) % cyclesDay.Length;
            globalLight.color = cyclesDay[actualCycle].cycleColor; // Actualizar el color inmediatamente
        }
    }

    private void UpdateTimeAndDayText()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día
        int displayHour = totalMinutes / 60;
        int displayMinute = totalMinutes % 60;

        string period = displayHour >= 12 ? "PM" : "AM";
        displayHour = displayHour > 12 ? displayHour - 12 : displayHour;
        displayHour = displayHour == 0 ? 12 : displayHour;

        timeText.text = $"Día {currentDay}";
    }

    private void SaveCurrentDay()
    {
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
        }
    }

    public bool IsNight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440;

        return (totalMinutes >= 1140 || totalMinutes < 300);
    }

    public bool IsAfter10PM()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440;

        return totalMinutes >= 1320;
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private int actualCycle = 0;
    private int nextCycle = 1;
    public static int currentDay = 1;
    public static float gameTimeInMinutes = 300f;

    private void Start()
    {
        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        // Establecer el color de la luz según el ciclo inicial
        globalLight.color = cyclesDay[0].cycleColor;
        UpdateTimeAndDayText(); // Actualizar el texto al iniciar
    }

    private void Update()
    {
        // Incrementar el tiempo del juego en minutos, basado en el tiempo transcurrido
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        if (gameTimeInMinutes >= 1439) // Si es 11:59 PM en el reloj del juego
        {
            currentDay++;
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        // Actualizar la luz según el tiempo actual
        UpdateDayStageAndLight();

        // Actualizar el texto del tiempo y día
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        // Verificar el tiempo y ajustar la luz de acuerdo con el ciclo
        if (totalMinutes >= 300 && totalMinutes < 480) // 5:00 AM - 7:59 AM
        {
            SetDayStage(0); // Mañana
            //Activar sprite AgujaMorning (desactivar todos los sprites de tag aguja menos el de este nombre)
        }
        else if (totalMinutes >= 480 && totalMinutes < 960) // 8:00 AM - 3:59 PM
        {
            SetDayStage(1); // Día
            //Activar sprite AgujaDay (desactivar todos los sprites de tag aguja menos el de este nombre)
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080) // 4:00 PM - 5:59 PM
        {
            SetDayStage(2); // Tarde
            //Activar sprite AgujaEvening (desactivar todos los sprites de tag aguja menos el de este nombre)
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260) // 6:00 PM - 8:59 PM
        {
            SetDayStage(3); // Atardecer
            //Activar sprite AgujaNight (desactivar todos los sprites de tag aguja menos el de este nombre)
        }
        else // 9:00 PM - 4:59 AM
        {
            SetDayStage(4); // Noche
            //Activar sprite AgujaLateNight
        }
    }

    private void SetDayStage(int stageIndex)
    {
        if (actualCycle != stageIndex) // Solo cambiar si el ciclo es diferente
        {
            actualCycle = stageIndex;
            nextCycle = (stageIndex + 1) % cyclesDay.Length;
            globalLight.color = cyclesDay[actualCycle].cycleColor; // Actualizar el color inmediatamente
        }
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
        //timeText.text = $"Día {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
        timeText.text = $"Día {currentDay}";
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
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
        }
    }

    // Método auxiliar para determinar si es de noche
    public bool IsNight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        // Noche es entre 7 PM (1140 minutos) y 4:59 AM (299 minutos)
        return (totalMinutes >= 1140 || totalMinutes < 300);
    }

    public bool IsAfter10PM()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        // Verificamos si la hora es mayor o igual a 10 PM (22:00, que son 1320 minutos en el día)
        return totalMinutes >= 1320;
    }
}*/
