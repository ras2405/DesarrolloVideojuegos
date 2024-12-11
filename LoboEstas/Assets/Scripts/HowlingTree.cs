using UnityEngine;

public class HowlingTree : MonoBehaviour
{
    public float triggerDistance = 5f; // Distancia a la que se puede activar el sonido
    public float cooldownTime = 120f; // Tiempo de espera en segundos (2 minutos)
    public float fadeDistance = 10f; // Distancia máxima a la que el volumen será 0
    public Transform player; // Referencia al jugador
    private AudioSource audioSource; // AudioSource del árbol
    private float lastPlayTime = -Mathf.Infinity; // Tiempo de la última reproducción

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").transform; // Encuentra al jugador por su tag
        audioSource = GetComponent<AudioSource>(); // Obtén el AudioSource del árbol
        audioSource.loop = false; // Asegúrate de que no sea en loop
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Verifica si puede reproducir el sonido
        if (distance <= triggerDistance && Time.time >= lastPlayTime + cooldownTime)
        {
            PlayHowl();
        }

        // Ajusta el volumen según la distancia
        AdjustVolumeBasedOnDistance(distance);
    }

    private void PlayHowl()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // Reproduce el sonido
            lastPlayTime = Time.time; // Registra el tiempo actual como última reproducción
        }
    }

    private void AdjustVolumeBasedOnDistance(float distance)
    {
        if (distance > fadeDistance)
        {
            audioSource.volume = 0f; // Silencia completamente si está fuera del rango
        }
        else
        {
            // Calcula el volumen basado en la distancia (linealmente)
            float normalizedDistance = Mathf.Clamp01((fadeDistance - distance) / fadeDistance);
            audioSource.volume = normalizedDistance; // Ajusta el volumen
        }
    }
}
