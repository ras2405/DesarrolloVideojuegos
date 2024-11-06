using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public AudioSource audioSource;  // El componente AudioSource que reproduce el sonido
    public float activationRadius = 50f;  // Radio de activación en unidades (6 tiles de 0.64 x 0.64)

    private void Update()
    {
        // Calcular la distancia entre el jugador y la fuente de sonido
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del radio de activación, reproducir el sonido
        if (distanceToPlayer <= activationRadius)
        {
            if (!audioSource.isPlaying)  // Si el sonido no está reproduciéndose, iniciar la reproducción
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)  // Si el sonido está sonando y el jugador está fuera del radio, detenerlo
            {
                audioSource.Stop();
            }
        }
    }
}