using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private CycleDay[] cyclesDay; 
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject[] clockHands; 

    private int actualCycle = 0;
    private int nextCycle = 1;
    public static int currentDay = 1;
    public static float gameTimeInMinutes = 300f;

    private void Start()
    {
        LoadCurrentDay();
        globalLight.color = cyclesDay[0].cycleColor;
        UpdateTimeAndDayText();
        UpdateClockHands(0); 
    }

    private void Update()
    {
        gameTimeInMinutes += (Time.deltaTime / (7 * 60)) * 1140; 
        if (gameTimeInMinutes >= 1439)
        {
            currentDay++;
            gameTimeInMinutes = 0; 
        }
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.Save();
        UpdateDayStageAndLight();
        UpdateTimeAndDayText();
    }

    private void UpdateDayStageAndLight()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; 
        if (totalMinutes >= 300 && totalMinutes < 480) // 5:00 AM - 7:59 AM
        {
            SetDayStage(0); // Mañana
            UpdateClockHands(0); 
        }
        else if (totalMinutes >= 480 && totalMinutes < 960) // 8:00 AM - 3:59 PM
        {
            SetDayStage(1); // Día
            UpdateClockHands(1); 
        }
        else if (totalMinutes >= 960 && totalMinutes < 1080) // 4:00 PM - 5:59 PM
        {
            SetDayStage(2); // Tarde
            UpdateClockHands(2); 
        }
        else if (totalMinutes >= 1080 && totalMinutes < 1260) // 6:00 PM - 8:59 PM
        {
            SetDayStage(3); // Atardecer
            UpdateClockHands(3); 
        }
        else // 9:00 PM - 4:59 AM
        {
            SetDayStage(4); // Noche
            UpdateClockHands(4); 
        }
    }

    private void UpdateClockHands(int activeIndex)
    {
        for (int i = 0; i < clockHands.Length; i++)
        {
            clockHands[i].SetActive(i == activeIndex); 
        }
    }

    private void SetDayStage(int stageIndex)
    {
        if (actualCycle != stageIndex) 
        {
            actualCycle = stageIndex;
            nextCycle = (stageIndex + 1) % cyclesDay.Length;
            globalLight.color = cyclesDay[actualCycle].cycleColor; 
        }
    }

    private void UpdateTimeAndDayText()
    {
        int totalMinutes = Mathf.FloorToInt(gameTimeInMinutes) % 1440; 
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