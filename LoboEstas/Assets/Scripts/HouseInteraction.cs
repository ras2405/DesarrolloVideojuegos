using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public bool insideHouse = false; // Para saber si est� dentro de la casa
    public Vector2 housePos = new Vector2(-25f, -2);
    public Vector2 forestPos = new Vector2(1f, 1f);
    public VideoPlayer videoPlayer; 

    public VideoClip[] videoClips;
    public Canvas videoCanvas;

    public RawImage rawImage;
    
    public GameObject brokenWindow;
    public GameObject reinforcedWindow;

    public GameObject brokenDoor;
    public GameObject reinforcedDoor;

    public GameObject fire;

    public GameObject deadPanel;
    public GameObject deathTextPanel;
    public TextMeshProUGUI deathText;


    private bool breakDoor = true;
    private bool breakWindow = true;
    private bool canInteract = false; // Si el jugador puede interactuar con la casa
    private bool reinforcedDoorCheck = true;
    private bool reinforcedWindowCheck = true;
    private bool fireOnCheck = true;
    private bool textDisplayed = false;
    private bool video1 = true;
    private bool video2 = true;
    private bool video3 = true;
    private float displayTime = 7f;
    public float duration = 5f;

    public AudioSource audioSource;
    public AudioClip doorSound;
    public AudioClip songAmbient;

    private bool isVideoPlaying = false;

    void Start()
    {
        deathTextPanel.SetActive(false);
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        videoCanvas.gameObject.SetActive(false);
        AdjustVideoAspect();
        videoPlayer.loopPointReached += OnVideoEnd;
        isVideoPlaying = false;
    }
    void Update()
    {
        // Si el jugador est� cerca y puede interactuar
        if (canInteract)
        {
            if (!insideHouse)
            {
                StartCoroutine(TeleportPlayer(housePos));
            }
            else
            {
                StartCoroutine(TeleportPlayer(forestPos));
            }
            if (audioSource != null && doorSound != null)
            {
                audioSource.PlayOneShot(doorSound);
            }
        }
        if(CycleDayController.currentDay == 2)
        {
            TransitionVideo2();
        } 
        if(CycleDayController.currentDay == 3)
        {
            TransitionVideo3();
        } 
        
        if(CycleDayController.currentDay == 4)
        {
            IsDoorReinforced();
        }
        if(CycleDayController.currentDay == 5)
        {
            IsWindowReinforced();
        }
        if(CycleDayController.currentDay == 6)
        {
            IsFireOn();
        }

        if(videoPlayer.isPlaying && !textDisplayed)
        {
            if(videoPlayer.time >= displayTime)
            {
                 StartCoroutine(ShowText());
            }
        }
    }

    private IEnumerator ShowText()
    {
        textDisplayed = true; // Evita que el texto se muestre varias veces

        deathTextPanel.SetActive(true); // Activa el texto
        yield return new WaitForSeconds(duration); // Espera el tiempo que debe estar visible
        deathTextPanel.SetActive(false); // Oculta el texto
    }

    void AdjustVideoAspect()
    {
        // Obtén las dimensiones del video
        float videoWidth = videoPlayer.targetTexture.width;
        float videoHeight = videoPlayer.targetTexture.height;

        // Obtén las dimensiones de la pantalla
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calcula las proporciones
        float videoAspectRatio = videoWidth / videoHeight;
        float screenAspectRatio = screenWidth / screenHeight;

        // Ajusta el tamaño del Raw Image según las proporciones
        if (videoAspectRatio > screenAspectRatio)
        {
            rawImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenWidth / videoAspectRatio);
        }
        else
        {
            rawImage.rectTransform.sizeDelta = new Vector2(screenHeight * videoAspectRatio, screenHeight);
        }
    }


    private void TransitionVideo2()
    {
         if(video2)
        {
            if (audioSource != null && songAmbient != null)
            {
                audioSource.PlayOneShot(songAmbient);
            }
            isVideoPlaying = true;
            video2 =false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[2];
            videoPlayer.Play();
        }
    }
    private void TransitionVideo3()
    {
         if(video3)
        {
            if (audioSource != null && songAmbient != null)
            {
                audioSource.PlayOneShot(songAmbient);
            }
            isVideoPlaying = true;
            video3 =false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[2];
            videoPlayer.Play();
        }
    }

     void OnVideoEnd(VideoPlayer vp)
    {
        isVideoPlaying = false;
        Debug.Log("El video ha terminado.");
        videoPlayer.gameObject.SetActive(false); // Desactiva el VideoPlayer
        videoCanvas.gameObject.SetActive(false); // Desactiva el Canvas
    }

    private void IsFireOn()
    {
        if (audioSource != null && songAmbient != null)
        {
            audioSource.PlayOneShot(songAmbient);
        }
        isVideoPlaying = true;
        if (!fire.activeSelf && fireOnCheck)
        {
            deathText.text = "No protegiste tu chimenea...";
            fireOnCheck = false;
            videoCanvas.gameObject.SetActive(true); 
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[1];
            videoPlayer.Play();
            deadPanel.SetActive(true); 
        }
        else if(fire.activeSelf && fireOnCheck){
            deathText.text = "";
            fireOnCheck = false;
            videoCanvas.gameObject.SetActive(true); 
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[0];
            videoPlayer.Play();
            deadPanel.SetActive(true); 
        }
    }

    private void IsDoorReinforced()
    {
        isVideoPlaying = true;
        if (!reinforcedDoor.activeSelf && reinforcedDoorCheck)
        {
            deathText.text = "No protegiste tu puerta...";
            reinforcedDoorCheck = false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[1];
            videoPlayer.Play();
            deadPanel.SetActive(true);
        }
        else if(reinforcedDoorCheck) {
            if (audioSource != null && songAmbient != null)
            {
                audioSource.PlayOneShot(songAmbient);
            }
            reinforcedDoorCheck =false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[2];
            BreakDoor();
        }
    }

    private void IsWindowReinforced()
    {
        isVideoPlaying = true;
        if (!reinforcedWindow.activeSelf && reinforcedWindowCheck)
        {
            deathText.text = "No protegiste tu ventana...";
            reinforcedWindowCheck = false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[1];
            videoPlayer.Play();
            deadPanel.SetActive(true);
        }
        else if(reinforcedWindowCheck){
            if (audioSource != null && songAmbient != null)
            {
                audioSource.PlayOneShot(songAmbient);
            }
            reinforcedWindowCheck = false;
            videoCanvas.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[2];
            BreakWindow();
        }
    }


    private void BreakWindow()
    {
        
        while(breakWindow)
        {
            brokenWindow.gameObject.SetActive(true);
            breakWindow = false;
        } 
    }

    private void BreakDoor()
    {
        while(breakDoor)
        {
            brokenDoor.gameObject.SetActive(true);
            breakDoor = false;
        }
    }

    void EnterHouse()
    {
        if (audioSource != null && doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
        }
        insideHouse = true;
        TeleportPlayer(housePos);

        Debug.Log("Entraste a la casa");
    }

    void ExitHouse()
    {
        if (audioSource != null && doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
        }
        insideHouse = false;

        TeleportPlayer(forestPos);
        Debug.Log("Saliste de la casa");
    }

    IEnumerator TeleportPlayer(Vector2 targetPosition)
    {
        Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.enabled = false; // Desactiva la c�mara moment�neamente

        yield return new WaitForEndOfFrame(); // Espera un frame

        player.transform.position = targetPosition; // Teletransporta al jugador

        virtualCamera.enabled = true; // Reactiva la c�mara
    }


    // Detecta si el jugador entra en el �rea de la puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = true; // Permite que el jugador pueda entrar a la casa
        }
    }

    // Detecta si el jugador sale del �rea de la puerta
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = false; // Impide que el jugador interact�e
        }
    }

    public bool IsVideoPlaying()
    {
        //bool isPlayingCinematic = videoPlayer.isPlaying;
        Debug.Log("!videoPlayer.isPlaying ,videoPlayer.time >= videoPlayer.length: " +
            videoPlayer.isPlaying + "," + videoPlayer.time +","+ videoPlayer.length);
        //!videoPlayer.isPlaying && 
        /*  if (videoPlayer.time >= videoPlayer.length)
          {
              Debug.Log("El video ha terminado.");
              return false;
              // Lógica adicional
          }
          else {
              return true;
          }*/
        //  Debug.Log("y - HouseInteraction - IsVideoPlaying(): " + isVideoPlaying);
        //  return isVideoPlaying;
        return videoPlayer.time >= videoPlayer.length;
    }

    public double VideoLength()
    {
        return videoPlayer.length;
    }


}
