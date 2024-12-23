
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public Transform player;                
    public AudioSource audioSource;          
    public GameObject marcoObject;         
    public AudioClip wolfMusic;           
    public AudioClip ambientSound;        
    public AudioClip bruma;                
    public float activationRadius = 50f;     // Radio de activación en unidades (6 tiles de 0.64 x 0.64)

    private bool isPlayingAmbientSound1 = true; 
    private bool isPlayingWolfMusic = false;   

    private void Start()
    {
        PlayAmbientSound();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (marcoObject != null && marcoObject.activeSelf)
        {
            if (!isPlayingWolfMusic)
            {
                StartWolfMusic();
            }
        }
        else
        {
            if (isPlayingWolfMusic)
            {
                StopWolfMusic();
                PlayAmbientSound(); 
            }

            if (!audioSource.isPlaying && !isPlayingWolfMusic)
            {
                PlayAmbientSound();
            }
        }

        if (distanceToPlayer > activationRadius && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void StartWolfMusic()
    {
        isPlayingWolfMusic = true;
        audioSource.clip = wolfMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void StopWolfMusic()
    {
        isPlayingWolfMusic = false;
        audioSource.Stop(); 
    }

    private void PlayAmbientSound()
    {
        if (isPlayingAmbientSound1)
        {
            audioSource.clip = ambientSound; // Cambiar al sonido ambiental 1
        }
        else
        {
            audioSource.clip = bruma; // Cambiar al sonido ambiental 2
        }

        isPlayingAmbientSound1 = !isPlayingAmbientSound1; 
        audioSource.loop = false;
        audioSource.Play();
    }
}