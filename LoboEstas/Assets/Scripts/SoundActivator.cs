using UnityEngine;

public class SoundActivator : MonoBehaviour
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
}