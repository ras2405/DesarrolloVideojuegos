using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    public Transform player;  
    public AudioSource audioSource; 
    public float activationRadius = 50f;  

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationRadius)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying) 
            {
                audioSource.Stop();
            }
        }
    }
}