using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 2.5f;
    public float captureDistance = 0.7f;
    public float transitionDistance = 3.5f;
    public float reducedSpeed = 0.9f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource wolfAudioSource;

    private CycleDayController cycleDayController;
    private HouseInteraction houseInteraction;

    public GameObject marco;

    public GameObject deadPanel; 

    private bool isWolfActive = false;
    private bool isNight = false;
    private bool isDead = false;
    private Vector3 lastPosition;

    private float lastDistance = Mathf.Infinity;

    public AudioSource branchBreakSound;  

    private bool hasBranchSoundPlayed = false;

    private bool isVidPlaying = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        wolfAudioSource = GetComponent<AudioSource>(); 

        DeactivateWolf();

        lastPosition = transform.position;

        cycleDayController = FindObjectOfType<CycleDayController>();
        houseInteraction = FindObjectOfType<HouseInteraction>();

        if (marco != null)
        {
            marco.SetActive(false);
        }

        if (deadPanel != null)
        {
            deadPanel.SetActive(false);
        }
    }

    void Update()
    {
        isVidPlaying = houseInteraction.isVideoPlaying;
        if (!isDead)
        {
            isNight = cycleDayController.IsNight();

            if (cycleDayController != null && isNight)
            {

                if (!isWolfActive)
                {
                    if (branchBreakSound != null && !hasBranchSoundPlayed)
                    {
                        branchBreakSound.Play();
                        hasBranchSoundPlayed = true; 
                    }

                    ActivateWolf();
                }

                if (isWolfActive && player != null)
                {
                    if (player.position.x >= -12f && player.position.x <= 12f &&
                        player.position.y >= -12f && player.position.y <= 12f)
                    {
                        Vector3 targetPosition = player.position + offset;

                        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                        float currentSpeed = followSpeed;
                        if (distanceToPlayer < transitionDistance)
                        {
                            currentSpeed = reducedSpeed;
                            if (lastDistance >= transitionDistance) 
                            {
                                animator.SetBool("isNear", true); 
                            }
                        }
                        else
                        {
                            if (lastDistance < transitionDistance) 
                            {
                                animator.SetBool("isNear", false); 
                            }
                        }

                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

                        if (transform.position.x > lastPosition.x)
                        {
                            spriteRenderer.flipX = false;  // El lobo se mueve hacia la derecha
                        }
                        else if (transform.position.x < lastPosition.x)
                        {
                            spriteRenderer.flipX = true;   // El lobo se mueve hacia la izquierda
                        }

                        lastPosition = transform.position;

                        lastDistance = distanceToPlayer;

                        if (Vector3.Distance(transform.position, player.position) < captureDistance)
                        {
                            if (deadPanel != null)
                            {
                                deadPanel.SetActive(true);
                            }
                            DeactivateWolf();
                            isDead = true;
                        }
                    }
                    else
                    {
                        DeactivateWolf();
                    }
                }
            }
            else
            {
                DeactivateWolf();
            }
        } else
        {
            DeactivateWolf();
        }
    }

    void DeactivateWolf()
    {
        spriteRenderer.enabled = false;
        animator.enabled = false;
        wolfAudioSource.enabled = false;

        isWolfActive = false;

        if (marco != null)
        {
            marco.SetActive(false);
        }

        float minX = -12f;
        float maxX = 13f;
        float minY = -8f;
        float maxY = 13f;

        int edge = Random.Range(0, 3); // 0 = Izquierda, 1 = Derecha, 2 = Arriba

        float spawnX = 0f;
        float spawnY = 0f;

        switch (edge)
        {
            case 0: // Borde Izquierdo
                spawnX = minX;
                spawnY = Random.Range(minY, maxY);
                break;

            case 1: // Borde Derecho
                spawnX = maxX;
                spawnY = Random.Range(minY, maxY);
                break;

            case 2: // Borde Superior
                spawnX = Random.Range(minX, maxX);
                spawnY = maxY;
                break;
        }

        transform.position = new Vector3(spawnX, spawnY, transform.position.z);
    }

    void ActivateWolf()
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        wolfAudioSource.enabled = true;

        isWolfActive = true;

        if (marco != null)
        {
            marco.SetActive(true);
        }

        if (wolfAudioSource != null && !wolfAudioSource.isPlaying)
        {
            wolfAudioSource.Play();
        }
    }
}


