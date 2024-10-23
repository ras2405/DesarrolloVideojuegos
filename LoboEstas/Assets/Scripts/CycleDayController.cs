/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del d�a
    [SerializeField] private float timePerCycle; // Duraci�n de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    private int currentDay = 1; // Contador para el d�a actual
    private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Start()
    {
        globalLight.color = cyclesDay[0].cycleColor;
        timePerCycle = 120f; // Establecer el tiempo para el ciclo Day (2 minutos)
        UpdateTimeAndDayText(); // Actualiza el texto al iniciar
    }

    // Hacemos que el tiempo aumente
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

            UpdateTimeAndDayText(); // Actualizar el texto del d�a
        }

        // Incrementar el tiempo del juego en minutos, basado en el tiempo transcurrido
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        // Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();

        // Comprobar si es 11:59 PM para incrementar el d�a
        if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
        {
            currentDay++;
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
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
        timeText.text = $"Día {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
    }
}*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    private int currentDay = 1; // Contador para el día actual
    private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Start()
    {
        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        globalLight.color = cyclesDay[0].cycleColor;
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
            PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el día actual
            PlayerPrefs.Save(); // Asegúrate de guardar los cambios
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }

        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios

        // Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();

        // Comprobar si es 11:59 PM para incrementar el día
      //  if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
      //  {
      //      currentDay++;
      //      gameTimeInMinutes = 0; // Reiniciar el tiempo del juego

            // Guardar el día actual en PlayerPrefs
      //      SaveCurrentDay();
      //  }

    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
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
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private static CycleDayController instance; // Instancia estática para el patrón Singleton
    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    private int currentDay = 1; // Contador para el día actual
    private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Awake()
    {
        // Implementar patrón Singleton para asegurar que solo haya una instancia de CycleDayController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    private void Start()
    {
        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        globalLight.color = cyclesDay[0].cycleColor;
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
          //  PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el día actual
           // PlayerPrefs.Save(); // Asegúrate de guardar los cambios
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay); // Guarda el día actual
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios

        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios

        // Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
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
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private static CycleDayController instance; // Instancia estática para el patrón Singleton
    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    private int currentDay = 1; // Contador para el día actual
    private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Awake()
    {
        // Implementar patrón Singleton para asegurar que solo haya una instancia de CycleDayController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    private void Start()
    {
        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        globalLight.color = cyclesDay[0].cycleColor;
        //timePerCycle = 120f; // Establecer el tiempo para el ciclo Day (2 minutos)
        UpdateTimeAndDayText(); // Actualiza el texto al iniciar
    }

    private void Update()
    {
        actualTimeCycle += Time.deltaTime;

        // Cambiar de ciclo si se ha completado el tiempo para el ciclo actual
        if (actualTimeCycle >= timePerCycle)
        {
            actualTimeCycle = 0; // Reiniciar el contador
            actualCycle = nextCycle; // Avanzar al siguiente ciclo

            // Ajustar el siguiente ciclo
            nextCycle = (nextCycle + 1) % cyclesDay.Length;

            UpdateTimeAndDayText(); // Actualizar el texto del día
        }

        // Incrementar el tiempo del juego en minutos
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        // Cuando llega a las 11:59 PM se inicia un nuevo día
        if (gameTimeInMinutes >= 1439)
        {
            currentDay++;
            gameTimeInMinutes = 0;
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        // Determinar la etapa del día y cambiar la luz global
        UpdateDayStageAndLight();

        // Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        if (totalMinutes >= 300 && totalMinutes < 480) // De 5 AM a 8 AM
        {
            Debug.Log("Se cambia a Sunrise");
            SetDayStage(0); // Sunrise
            timePerCycle = 180f; // Duración de Sunrise
        }
        else if (totalMinutes >= 480 && totalMinutes < 960) // De 8 AM a 4 PM
        {
            Debug.Log("Se cambia a Day");
            SetDayStage(1); // Day
            timePerCycle = 480f; // Duración de Day
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080) // De 4 PM a 6 PM
        {
            Debug.Log("Se cambia a Sunset");
            SetDayStage(2); // Sunset
            timePerCycle = 120f; // Duración de Sunset
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260) // De 6 PM a 9 PM
        {
            Debug.Log("Se cambia a Night");
            SetDayStage(3); // Night
            timePerCycle = 180f; // Duración de Night
        }
        else // De 9 PM a 5 AM
        {
            Debug.Log("Se cambia a LateNight");
            SetDayStage(4); // LateNight
            timePerCycle = 240f; // Duración de LateNight
        }
    }

    private void SetDayStage(int stageIndex)
    {
        // Establecer el ciclo actual según la etapa del día
        actualCycle = stageIndex;
        nextCycle = (stageIndex + 1) % cyclesDay.Length; // Determinar el siguiente ciclo
        globalLight.color = cyclesDay[actualCycle].cycleColor; // Cambiar color de luz según la etapa
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        //Debug.Log("averageCycle: " + averageCycle);
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
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
    }
}*/

/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{

    public static CycleDayController Instance { get; private set; }

    [SerializeField] private Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    //private static CycleDayController instance; // Instancia estática para el patrón Singleton
    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    private int currentDay = 1; // Contador para el día actual
    private float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Awake()
    {
        // Implementar patrón Singleton para asegurar que solo haya una instancia de CycleDayController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }

        // Verificar si las referencias están asignadas
        if (globalLight == null)
        {
            Debug.LogError("globalLight no está asignado en el Inspector.");
        }

        if (cyclesDay == null || cyclesDay.Length == 0)
        {
            Debug.LogError("cyclesDay no está asignado o el array está vacío.");
        }

        if (timeText == null)
        {
            Debug.LogError("timeText no está asignado en el Inspector.");
        }
    }

    private void OnEnable()
    {
        // Reasignar globalLight si se pierde la referencia
        if (globalLight == null)
        {
            globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
            if (globalLight == null)
            {
                Debug.LogError("No se pudo encontrar el objeto GlobalLight en la escena.");
            }
        }

        // Reasignar timeText si se pierde la referencia
        if (timeText == null)
        {
            timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
            if (timeText == null)
            {
                Debug.LogError("No se pudo encontrar el objeto TimeText en la escena.");
            }
        }
    }

    public void ResetCycle()
    {
        Debug.Log("Llama a ResetCycle");
        actualTimeCycle = 0;
        actualCycle = 0;
        nextCycle = 1;
        currentDay = 1;
        gameTimeInMinutes = 300f; // Reiniciar el tiempo del juego a 5 AM
        if (globalLight != null && cyclesDay.Length > 0)
        {
            globalLight.color = cyclesDay[0].cycleColor; // Restablecer el color de la luz inicial
        }
        UpdateTimeAndDayText(); // Actualizar el texto del día y la hora
    }

    private void Start()
    {
        // Verificar y asignar referencias si es necesario
        if (globalLight == null)
        {
            globalLight = FindObjectOfType<Light2D>(); // O encuentra la luz en la escena de alguna otra forma
        }

        if (cyclesDay == null || cyclesDay.Length == 0)
        {
            // Aquí puedes inicializar manualmente si es necesario o lanzar un error
            Debug.LogError("cyclesDay no está configurado correctamente.");
        }

        // Verificar y asignar el timeText si no está asignado
        if (timeText == null)
        {
            timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
            if (timeText == null)
            {
                Debug.LogError("No se pudo encontrar el objeto TimeText en la escena.");
            }
        }


        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        globalLight.color = cyclesDay[0].cycleColor;
        UpdateTimeAndDayText(); // Actualiza el texto al iniciar
    }

    private void Update()
    {
        actualTimeCycle += Time.deltaTime;

        // Cambiar de ciclo si se ha completado el tiempo para el ciclo actual
        if (actualTimeCycle >= timePerCycle)
        {
            actualTimeCycle = 0; // Reiniciar el contador
            actualCycle = nextCycle; // Avanzar al siguiente ciclo

            // Ajustar el siguiente ciclo
            nextCycle = (nextCycle + 1) % cyclesDay.Length;

            UpdateTimeAndDayText(); // Actualizar el texto del día
        }

        // Incrementar el tiempo del juego en minutos
        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // 5 minutos en tiempo real = 1440 minutos en el juego

        // Cuando llega a las 11:59 PM se inicia un nuevo día
        if (gameTimeInMinutes >= 1439)
        {
            currentDay++;
            gameTimeInMinutes = 0;
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        // Determinar la etapa del día y cambiar la luz global
        UpdateDayStageAndLight();

        // Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);

        // Actualizar el texto de la hora
        UpdateTimeAndDayText();
    }

    // Nuevo método para establecer el día y el tiempo
    public void SetDayAndTime(int day, float timeInMinutes)
    {
        currentDay = day;
        gameTimeInMinutes = timeInMinutes;

        // Calcular el ciclo actual basado en el tiempo
        UpdateDayStageAndLight();

        // Actualiza el texto al establecer el día y el tiempo
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        Debug.Log("gameTimeInMinutes en UpdateDayStageAndLight" + gameTimeInMinutes);
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; // 1440 minutos en un día

        if (totalMinutes >= 300 && totalMinutes < 480) // De 5 AM a 8 AM
        {
            SetDayStage(0); // Sunrise
            timePerCycle = 180f; // Duración de Sunrise
        }
        else if (totalMinutes >= 480 && totalMinutes < 960) // De 8 AM a 4 PM
        {
            SetDayStage(1); // Day
            timePerCycle = 480f; // Duración de Day
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080) // De 4 PM a 6 PM
        {
            SetDayStage(2); // Sunset
            timePerCycle = 120f; // Duración de Sunset
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260) // De 6 PM a 9 PM
        {
            SetDayStage(3); // Night
            timePerCycle = 180f; // Duración de Night
        }
        else // De 9 PM a 5 AM
        {
            SetDayStage(4); // LateNight
            timePerCycle = 240f; // Duración de LateNight
        }
    }

    private void SetDayStage(int stageIndex)
    {
        // Establecer el ciclo actual según la etapa del día
        Debug.Log("Llama a SetDayStage con stageIndex: " + stageIndex);
        actualCycle = stageIndex;
        nextCycle = (stageIndex + 1) % cyclesDay.Length; // Determinar el siguiente ciclo
        
        globalLight.color = cyclesDay[actualCycle].cycleColor; // Cambiar color de luz según la etapa
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
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
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para manejar las escenas

public class CycleDayController : MonoBehaviour
{
    public static CycleDayController Instance { get; private set; }

    [SerializeField] public static Light2D globalLight; // Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay; // Referencia a los ciclos del día
    [SerializeField] private float timePerCycle; // Duración de cada ciclo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al texto de hora

    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;
    public static int currentDay = 1; // Contador para el día actual
    public static float gameTimeInMinutes = 300f; // Inicializa el tiempo en 5 AM (300 minutos en el reloj del juego)

    private void Awake()
    {
        // Implementar patrón Singleton para asegurar que solo haya una instancia de CycleDayController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
            return;
        }

        // Verificar si las referencias están asignadas
        if (globalLight == null)
        {
            Debug.LogError("globalLight no está asignado en el Inspector.");
        }

        if (cyclesDay == null || cyclesDay.Length == 0)
        {
            Debug.LogError("cyclesDay no está asignado o el array está vacío.");
        }

        if (timeText == null)
        {
            Debug.LogError("timeText no está asignado en el Inspector.");
        }
    }

    private void OnEnable()
    {
        // Reasignar globalLight si se pierde la referencia
        if (globalLight == null)
        {
            globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
            if (globalLight == null)
            {
                Debug.LogError("No se pudo encontrar el objeto GlobalLight en la escena.");
            }
        }

        // Reasignar timeText si se pierde la referencia
        if (timeText == null)
        {
            timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
            if (timeText == null)
            {
                Debug.LogError("No se pudo encontrar el objeto TimeText en la escena.");
            }
        }

        // Escuchar el cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Dejar de escuchar el cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método para destruir el controlador en la escena "Main Menu"
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            // Si estamos en la escena de Main Menu, destruir el objeto
            Destroy(gameObject);
        }
    }

    public void ResetCycle()
    {
        Debug.Log("Llama a ResetCycle");
        actualTimeCycle = 0;
        actualCycle = 0;
        nextCycle = 1;
        currentDay = 1;
        gameTimeInMinutes = 300f; // Reiniciar el tiempo del juego a 5 AM
        if (globalLight != null && cyclesDay.Length > 0)
        {
            globalLight.color = cyclesDay[0].cycleColor; // Restablecer el color de la luz inicial
        }
        UpdateTimeAndDayText(); // Actualizar el texto del día y la hora
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); 
        // Verificar y asignar referencias si es necesario
        if (globalLight == null)
        {
            globalLight = FindObjectOfType<Light2D>(); // O encuentra la luz en la escena de alguna otra forma
        }

        if (cyclesDay == null || cyclesDay.Length == 0)
        {
            Debug.LogError("cyclesDay no está configurado correctamente.");
        }

        if (timeText == null)
        {
            timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
            if (timeText == null)
            {
                Debug.LogError("No se pudo encontrar el objeto TimeText en la escena.");
            }
        }

        // Cargar el día desde PlayerPrefs
        LoadCurrentDay();

        globalLight.color = cyclesDay[0].cycleColor;
        UpdateTimeAndDayText(); // Actualiza el texto al iniciar
    }

    private void Update()
    {
        actualTimeCycle += Time.deltaTime;

        if (actualTimeCycle >= timePerCycle)
        {
            actualTimeCycle = 0; // Reiniciar el contador
            actualCycle = nextCycle; // Avanzar al siguiente ciclo

            nextCycle = (nextCycle + 1) % cyclesDay.Length;
            UpdateTimeAndDayText(); // Actualizar el texto del día
        }

        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; // Incrementar el tiempo en el juego

        if (gameTimeInMinutes >= 1439)
        {
            currentDay++;
            gameTimeInMinutes = 0;
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();

        UpdateDayStageAndLight();
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);
        UpdateTimeAndDayText();
    }

    // Nuevo método para establecer el día y el tiempo
    public void SetDayAndTime(int day, float timeInMinutes)
    {
        currentDay = day;
        gameTimeInMinutes = timeInMinutes;

        // Calcular el ciclo actual basado en el tiempo
        UpdateDayStageAndLight();

        // Actualiza el texto al establecer el día y el tiempo
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440;

        if (totalMinutes >= 300 && totalMinutes < 480)
        {
            SetDayStage(0);
            timePerCycle = 180f;
        }
        else if (totalMinutes >= 480 && totalMinutes < 960)
        {
            SetDayStage(1);
            timePerCycle = 480f;
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080)
        {
            SetDayStage(2);
            timePerCycle = 120f;
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260)
        {
            SetDayStage(3);
            timePerCycle = 180f;
        }
        else
        {
            SetDayStage(4);
            timePerCycle = 240f;
        }
    }

    private void SetDayStage(int stageIndex)
    {
        actualCycle = stageIndex;
        nextCycle = (stageIndex + 1) % cyclesDay.Length;
        globalLight.color = cyclesDay[actualCycle].cycleColor;
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, averageCycle);
    }

    private void UpdateTimeAndDayText()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440;
        int displayHour = totalMinutes / 60;
        int displayMinute = totalMinutes % 60;

        string period = displayHour >= 12 ? "PM" : "AM";
        displayHour = displayHour > 12 ? displayHour - 12 : displayHour;
        displayHour = displayHour == 0 ? 12 : displayHour;

        timeText.text = $"Día {currentDay}, {displayHour:D2}:{displayMinute:D2} {period}";
    }

    private void LoadCurrentDay()
    {
        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
        }
    }
}