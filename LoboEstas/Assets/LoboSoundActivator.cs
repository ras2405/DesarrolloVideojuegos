/*using UnityEngine;

public class LoboSoundActivator : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public AudioSource audioSource;  // El componente AudioSource que reproduce el sonido
    public float activationRadius = 50f;  // Radio de activaci�n en unidades (6 tiles de 0.64 x 0.64)

    private void Update()
    {
        // Calcular la distancia entre el jugador y la fuente de sonido
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador est� dentro del radio de activaci�n, reproducir el sonido
        if (distanceToPlayer <= activationRadius)
        {
            if (!audioSource.isPlaying)  // Si el sonido no est� reproduci�ndose, iniciar la reproducci�n
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)  // Si el sonido est� sonando y el jugador est� fuera del radio, detenerlo
            {
                audioSource.Stop();
            }
        }
    }
}*//*

using UnityEngine;

public class LoboSoundActivator : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public AudioSource audioSource;  // El componente AudioSource que reproduce el sonido
    public float activationRadius = 50f;  // Radio de activaci�n en unidades (6 tiles de 0.64 x 0.64)
    public GameObject marcoObject;  // El objeto que contiene el marco (o el sprite del marco)

    private SpriteRenderer marcoSpriteRenderer;  // El SpriteRenderer del objeto marco

    private void Start()
    {
        // Aseguramos que estamos obteniendo el SpriteRenderer del objeto marco
        if (marcoObject != null)
        {
            marcoSpriteRenderer = marcoObject.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        // Verificar si el marco est� activado
        if (marcoSpriteRenderer != null && marcoSpriteRenderer.enabled)
        {
            // Calcular la distancia entre el jugador y la fuente de sonido
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Si el jugador est� dentro del radio de activaci�n, reproducir el sonido
            if (distanceToPlayer <= activationRadius)
            {
                if (!audioSource.isPlaying)  // Si el sonido no est� reproduci�ndose, iniciar la reproducci�n
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)  // Si el sonido est� sonando y el jugador est� fuera del radio, detenerlo
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            // Detener el sonido si el marco no est� visible
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}*/

using UnityEngine;

public class LoboSoundActivator : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public AudioSource audioSource;  // El componente AudioSource que reproduce el sonido
    public float activationRadius = 50f;  // Radio de activaci�n en unidades (6 tiles de 0.64 x 0.64)
    public GameObject marcoObject;  // El objeto que contiene el marco (o el sprite del marco)

    private void Update()
    {
        // Verificar si el objeto marco est� activado
        if (marcoObject != null && marcoObject.activeSelf)
        {
            // Calcular la distancia entre el jugador y la fuente de sonido
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Si el jugador est� dentro del radio de activaci�n, reproducir el sonido
            if (distanceToPlayer <= activationRadius)
            {
                if (!audioSource.isPlaying)  // Si el sonido no est� reproduci�ndose, iniciar la reproducci�n
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)  // Si el sonido est� sonando y el jugador est� fuera del radio, detenerlo
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            // Detener el sonido si el objeto marco no est� activo
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}