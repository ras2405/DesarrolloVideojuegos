using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSoundFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY = -15f;

    public AudioSource waterSound;

    public float maxVolumeDistance = -15f; 
    public float startAudibleDistance = 2f; 

    void Update()
    {
        if (player != null)
        {
            if (player.position.x >= -11f && player.position.x <= 11f &&
                    player.position.y >= -11f && player.position.y <= 11f)
            {
                float playerY = player.position.y;
                if (playerY > startAudibleDistance)
                {
                    waterSound.volume = 0;
                    return;
                }

                float volume = Mathf.InverseLerp(startAudibleDistance, maxVolumeDistance, playerY);
                waterSound.volume = volume - 0.5f;

                transform.position = new Vector2(player.position.x, fixedY);
            }
        }
    }
}
