using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSoundFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY = -15f;

    public AudioSource waterSound;

    // Valores para definir el rango de audibilidad
    public float maxVolumeDistance = -15f; // Cuando el sonido es máximo (nivel del agua)
    public float startAudibleDistance = 2f; // Cuando el sonido empieza a ser audible

    void Update()
    {
        if (player != null)
        {
            if (player.position.x >= -11f && player.position.x <= 11f &&
                    player.position.y >= -11f && player.position.y <= 11f)
            {
                // Obtener la posición Y del jugador
                float playerY = player.position.y;

                // Si el jugador está por encima del rango audible, no reproducir sonido
                if (playerY > startAudibleDistance)
                {
                    waterSound.volume = 0;
                    return;
                }

                // Calcular el volumen en función de la posición Y del jugador
                float volume = Mathf.InverseLerp(startAudibleDistance, maxVolumeDistance, playerY);
                waterSound.volume = volume - 0.5f;

                transform.position = new Vector2(player.position.x, fixedY);
            }
        }
    }
}
