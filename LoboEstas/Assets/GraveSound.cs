using UnityEngine;

public class GraveSound : MonoBehaviour
{
    public float triggerDistance = 5f; 
    public float cooldownTime = 120f; 
    public float fadeDistance = 10f; 
    public Transform player;
    private AudioSource audioSource;
    private float lastPlayTime = -Mathf.Infinity; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        audioSource.loop = false; 
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance && Time.time >= lastPlayTime + cooldownTime)
        {
            PlayGraveSound();
        }

        AdjustVolumeBasedOnDistance(distance);
    }

    private void PlayGraveSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); 
            lastPlayTime = Time.time; 
        }
    }

    private void AdjustVolumeBasedOnDistance(float distance)
    {
        if (distance > fadeDistance)
        {
            audioSource.volume = 0f; 
        }
        else
        {
            float normalizedDistance = Mathf.Clamp01((fadeDistance - distance) / fadeDistance);
            audioSource.volume = normalizedDistance; 
        }
    }
}
