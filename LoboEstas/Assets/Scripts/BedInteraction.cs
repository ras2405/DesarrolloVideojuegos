using UnityEngine;
using TMPro;
using System.Collections;

public class BedInteraction : MonoBehaviour
{
    public GameObject textPanel;  
    public TextMeshProUGUI dayText; 

    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI notNightText;

    public GameObject cutsceneBG;

    private CycleDayController cycleDayController;

    public KeyCode interactionKey = KeyCode.E;
    public GameObject character; 
    private int currentDay = 1; 
    public Vector2 bedPosition = new Vector2(-25, -1);

    private bool isNight = false;
    private bool isNearBed = false;

    public AudioSource audioSource; 
    public AudioClip[] dayTransitionClips; 
    public AudioSource houseAmbient;

    private HouseInteraction houseInteraction;

    private bool isVidPlaying = false;
    private PlayerController playerController;


    private void Start()
    {
        playerController = character.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("No se encontró PlayerController en el objeto del personaje.");
        }
        GameObject houseObject = GameObject.Find("house");
        if (houseObject != null)
        {
            houseInteraction = houseObject.GetComponent<HouseInteraction>();
        }

        if (houseInteraction == null)
        {
            Debug.LogError("No se encontró HouseInteraction en el objeto 'HouseObject'.");
        }
        LoadCurrentDay();
        Debug.Log("Se carga el día desde CycleDayController: " + currentDay);
        cycleDayController = FindObjectOfType<CycleDayController>();
        houseInteraction = FindObjectOfType<HouseInteraction>();
        textPanel.SetActive(false);
        cutsceneBG.SetActive(false);
        interactionText.gameObject.SetActive(false);
        notNightText.gameObject.SetActive(false);
    }

    private void Update()
    {
        isNight = cycleDayController.IsNight();
        isVidPlaying = houseInteraction.vidPlaying;
        if (isNearBed && cycleDayController.IsNight())
        {
            notNightText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(true); 
            if (Input.GetKeyDown(interactionKey)) 
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
            interactionText.gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = true;
            Debug.Log("El personaje esta tocando la cama");
        }
    }

    private void Dormir()
    {
        houseAmbient.gameObject.SetActive(false);
        currentDay++;
        CycleDayController.currentDay = currentDay; 
        CycleDayController.gameTimeInMinutes = 300f;
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();
        if (playerController != null)
        {
            playerController.RestoreStamina();
        }
        else
        {
            Debug.LogWarning("PlayerController no está asignado. No se pudo restaurar la stamina.");
        }
        Debug.Log("Se ejecuta WaitForSecondsExample()");
        cutsceneBG.SetActive(true);
        StartCoroutine(WaitForSecondsToClue());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == character)
        {
            isNearBed = false;
            Debug.Log("El personaje deja de tocar la cama");
            HideDayPanel();
        }
    }

    IEnumerator WaitForSecondsToClue()
    {
        yield return new WaitForSeconds((float)houseInteraction.VideoLength() - 4);
        Debug.Log("Video finalizado, ejecuta ShowClueOnScreen");
        ShowClueOnScreen(currentDay - 1);
    }

    private void MoveCharacterToBed()
    {
        character.transform.position = bedPosition; 
    }

    private void ShowClueOnScreen(int num)
    {
        if (!isVidPlaying)
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

            textPanel.SetActive(true); 
            dayText.text = "Día: " + currentDay.ToString() + clue;
            Debug.Log("Se activa el panel con Día: " + currentDay.ToString() + clue);
            Debug.Log("Ejecuta  StartCoroutine(WaitForAudioAndHidePanel(currentDay))");
            StartCoroutine(WaitForAudioAndHidePanel(currentDay));
        }
    }

    private IEnumerator WaitForAudioAndHidePanel(int day)
    {
        Debug.Log("WaitForAudioAndHidePanel, ejecuta el audio del mensaje:  PlayDayTransitionAudio(day)");
        yield return new WaitForSeconds(3f); 
        PlayDayTransitionAudio(day); 
        while (audioSource.isPlaying)
        {
            yield return null; 
        }
        yield return new WaitForSeconds(1f);
        HideDayPanel();
    }
    private void UpdateDayText()
    {
        dayText.text = "Dia: " + currentDay.ToString();
    }

    private void HideDayPanel()
    {
        cutsceneBG.SetActive(false);
        textPanel.SetActive(false); 
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio detenido al ocultar el panel.");
        }
    }

    private void LoadCurrentDay()
    {
        currentDay = CycleDayController.currentDay;
        Debug.Log("Dia cargado desde CycleDayController: " + currentDay);
    }

    private void PlayDayTransitionAudio(int day)
    {
        Debug.Log("Se llama a PlayDayTransitionAudio(int day)");
        int clipIndex = day - 2;
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
                Debug.Log($"Reproduciendo audio de transicion para el dia {day}");
                StartCoroutine(ResumeHouseAmbientAfterAudio());
            }
        }
        else
        {
            Debug.LogWarning($"No se encontro un clip para el dia {day} o AudioSource no asignado");
        }
    }

    private IEnumerator ResumeHouseAmbientAfterAudio()
    {
        while (audioSource.isPlaying)
        {
            yield return null; 
        }
        if (houseAmbient != null && !houseAmbient.gameObject.activeSelf)
        {
            houseAmbient.gameObject.SetActive(true);
            Debug.Log("GameObject 'houseAmbient' reactivado.");
        }
    }

}