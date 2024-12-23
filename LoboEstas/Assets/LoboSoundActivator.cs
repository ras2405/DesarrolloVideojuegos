using UnityEngine;

public class LoboSoundActivator : MonoBehaviour
{
    public Transform player;  
    public AudioSource audioSource; 
    public float activationRadius = 50f;  // Radio de activación en unidades (6 tiles de 0.64 x 0.64)
    public GameObject marcoObject;  

    private void Update()
    {
        if (marcoObject != null && marcoObject.activeSelf)
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
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}