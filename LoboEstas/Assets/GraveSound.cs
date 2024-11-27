using UnityEngine;

public class GraveSound : MonoBehaviour
{
    public float triggerDistance = 5f; // Distancia a la que se puede activar el sonido
    public float cooldownTime = 120f; // Tiempo de espera en segundos (2 minutos)
    public float fadeDistance = 10f; // Distancia m�xima a la que el volumen ser� 0
    private Transform player; // Referencia al jugador
    private AudioSource audioSource; // AudioSource de la tumba
    private float lastPlayTime = -Mathf.Infinity; // Tiempo de la �ltima reproducci�n

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Encuentra al jugador por su tag
        audioSource = GetComponent<AudioSource>(); // Obt�n el AudioSource de la tumba
        audioSource.loop = false; // Aseg�rate de que no sea en loop
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Verifica si puede reproducir el sonido
        if (distance <= triggerDistance && Time.time >= lastPlayTime + cooldownTime)
        {
            PlayGraveSound();
        }

        // Ajusta el volumen seg�n la distancia
        AdjustVolumeBasedOnDistance(distance);
    }

    private void PlayGraveSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // Reproduce el sonido
            lastPlayTime = Time.time; // Registra el tiempo actual como �ltima reproducci�n
        }
    }

    private void AdjustVolumeBasedOnDistance(float distance)
    {
        if (distance > fadeDistance)
        {
            audioSource.volume = 0f; // Silencia completamente si est� fuera del rango
        }
        else
        {
            // Calcula el volumen basado en la distancia (linealmente)
            float normalizedDistance = Mathf.Clamp01((fadeDistance - distance) / fadeDistance);
            audioSource.volume = normalizedDistance; // Ajusta el volumen
        }
    }
}
