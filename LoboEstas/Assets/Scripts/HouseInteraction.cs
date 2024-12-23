using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; 
    public bool insideHouse = false;
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
    public GameObject dayAudio;
    public TextMeshProUGUI deathText;


    private bool breakDoor = true;
    private bool breakWindow = true;
    private bool canInteract = false; 
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

    public bool isVideoPlaying = false;
    public bool vidPlaying = false;

    void Start()
    {
        deathTextPanel.SetActive(false);
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        videoCanvas.gameObject.SetActive(false);
        AdjustVideoAspect();
        videoPlayer.loopPointReached += OnVideoEnd;
        isVideoPlaying = false;
        vidPlaying = false;
    }
    void Update()
    {
        if (canInteract)
        {
            if (!insideHouse)
            {
                StartCoroutine(TeleportPlayer(housePos));
                dayAudio.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(TeleportPlayer(forestPos));
                dayAudio.gameObject.SetActive(true);
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
        textDisplayed = true;

        deathTextPanel.SetActive(true);
        yield return new WaitForSeconds(duration); 
        deathTextPanel.SetActive(false);
    }

    void AdjustVideoAspect()
    {
        float videoWidth = videoPlayer.targetTexture.width;
        float videoHeight = videoPlayer.targetTexture.height;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float videoAspectRatio = videoWidth / videoHeight;
        float screenAspectRatio = screenWidth / screenHeight;

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
        videoPlayer.gameObject.SetActive(false); 
        videoCanvas.gameObject.SetActive(false);
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
            vidPlaying = true;
            deadPanel.SetActive(true); 
        }
        else if(fire.activeSelf && fireOnCheck){
            deathText.text = "";
            fireOnCheck = false;
            videoCanvas.gameObject.SetActive(true); 
            videoPlayer.gameObject.SetActive(true); 
            videoPlayer.clip = videoClips[0];
            videoPlayer.Play();
            vidPlaying = true;
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
            vidPlaying = true;
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
            vidPlaying = true;
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
    }

    void ExitHouse()
    {
        if (audioSource != null && doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
        }
        insideHouse = false;

        TeleportPlayer(forestPos);
    }

    IEnumerator TeleportPlayer(Vector2 targetPosition)
    {
        Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.enabled = false; 

        yield return new WaitForEndOfFrame(); 

        player.transform.position = targetPosition; 

        virtualCamera.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = false; 
        }
    }

    public bool IsVideoPlaying()
    {
        Debug.Log("!videoPlayer.isPlaying ,videoPlayer.time >= videoPlayer.length: " +
            videoPlayer.isPlaying + "," + videoPlayer.time +","+ videoPlayer.length);
        return videoPlayer.time >= videoPlayer.length;
    }

    public double VideoLength()
    {
        return videoPlayer.length;
    }
}
