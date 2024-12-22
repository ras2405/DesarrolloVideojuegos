using UnityEngine;
using TMPro;

public class CycleDayHouseController : MonoBehaviour
{
    [SerializeField] private CycleDay[] cyclesDay; 
    [SerializeField] private float timePerCycle;
    [SerializeField] private TextMeshProUGUI timeText; 

    private float actualTimeCycle = 0;
    private float averageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;
    public static int currentDay = 1;
    public static float gameTimeInMinutes = 300f;

    private void Start()
    {
        currentDay = CycleDayController.currentDay;
        gameTimeInMinutes = CycleDayController.gameTimeInMinutes;

        timePerCycle = 120f; 
        UpdateTimeAndDayText(); 
    }

    private void Update()
    {
        actualTimeCycle += Time.deltaTime;
        averageCycle = actualTimeCycle / timePerCycle;
        if (actualTimeCycle >= timePerCycle)
        {
            actualTimeCycle = 0; 
            actualCycle = nextCycle;
            nextCycle = (nextCycle + 1) % cyclesDay.Length; 
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

        gameTimeInMinutes += (Time.deltaTime / (5 * 60)) * 1440; 

        if (gameTimeInMinutes >= 1439) // 11:59 PM en minutos
        {
            currentDay++;
            gameTimeInMinutes = 0; // Reiniciar el tiempo del juego
        }
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes); // Guarda el tiempo actual
        PlayerPrefs.Save(); 

        UpdateTimeAndDayText();

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
        CycleDayController.currentDay = currentDay;
        CycleDayController.gameTimeInMinutes = gameTimeInMinutes;
    }

    private void SaveCurrentDay()
    {
        Debug.Log("Se actualiza prefs: " + currentDay);
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save(); 
    }

    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
    }
}