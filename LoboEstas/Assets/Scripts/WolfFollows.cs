using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 2.5f;  // Velocidad normal del lobo
    public float captureDistance = 0.7f; // Distancia m�nima para atrapar al jugador
    public float transitionDistance = 3.5f; // Distancia para cambiar entre caminata1 y caminata2
    public float reducedSpeed = 0.9f; // Velocidad reducida cuando el lobo est� cerca del jugador

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource wolfAudioSource;

    private CycleDayController cycleDayController;
    private HouseInteraction houseInteraction;

    // Referencia al objeto "marco"
    public GameObject marco; // Asigna "marco" desde el Inspector

    // Referencia al DeadPanel
    public GameObject deadPanel; // Asigna el DeadPanel desde el Inspector

    private bool isWolfActive = false;
    private bool isNight = false;
    private bool isDead = false;
    private Vector3 lastPosition;

    // Variables para la histeresis
    private float lastDistance = Mathf.Infinity; // Distancia anterior para comparar

    // Agregamos las referencias para los sonidos de la rama y el aullido
    public AudioSource branchBreakSound;  // Sonido de rama quebr�ndose
                                          // Sonido de aullido

    // Para controlar el momento de la activaci�n de la rama
    private bool hasBranchSoundPlayed = false;

    private bool isVidPlaying = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        wolfAudioSource = GetComponent<AudioSource>(); // Obtener el componente de audio del lobo

        // Desactivar todo lo relacionado con el lobo al inicio
        DeactivateWolf();

        // Guarda la posici�n inicial
        lastPosition = transform.position;

        // Busca el controlador del ciclo del d�a en la escena
        cycleDayController = FindObjectOfType<CycleDayController>();
        houseInteraction = FindObjectOfType<HouseInteraction>();

        // "marco" est� desactivado al iniciar
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // DeadPanel est� desactivado al iniciar
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
            // Verificar si es de noche
            if (cycleDayController != null && isNight)
            {
                // Si el lobo no est� activo y es de noche, activarlo
                if (!isWolfActive)
                {
                    // Reproducir el sonido de la rama quebr�ndose solo una vez antes de la activaci�n
                    if (branchBreakSound != null && !hasBranchSoundPlayed)
                    {
                        branchBreakSound.Play();
                        hasBranchSoundPlayed = true;  // Aseguramos que solo se reproduzca una vez
                    }

                    // Activar al lobo
                    ActivateWolf();
                }

                if (isWolfActive && player != null)
                {
                    // Verificar si el jugador est� dentro del �rea de -11f y 11f en X e Y
                    if (player.position.x >= -12f && player.position.x <= 12f &&
                        player.position.y >= -12f && player.position.y <= 12f)
                    {
                        // Calcula la nueva posici�n hacia el jugador
                        Vector3 targetPosition = player.position + offset;

                        // Calcula la distancia entre el lobo y el jugador
                        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                        // Reduce la velocidad si el lobo est� cerca del jugador (en la animaci�n caminata2)
                        float currentSpeed = followSpeed;
                        if (distanceToPlayer < transitionDistance)
                        {
                            currentSpeed = reducedSpeed;
                            if (lastDistance >= transitionDistance) // Histeresis: solo cambia si estamos fuera de la zona
                            {
                                animator.SetBool("isNear", true); // Cambia la animaci�n cuando est� cerca
                            }
                        }
                        else
                        {
                            if (lastDistance < transitionDistance) // Histeresis: solo cambia si estamos dentro de la zona
                            {
                                animator.SetBool("isNear", false); // Vuelve a la animaci�n caminata1
                            }
                        }

                        // Usamos MoveTowards para asegurarnos de que el lobo se acerque hasta el jugador
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

                        // Actualiza la direcci�n del sprite del lobo seg�n su movimiento
                        if (transform.position.x > lastPosition.x)
                        {
                            spriteRenderer.flipX = false;  // El lobo se mueve hacia la derecha
                        }
                        else if (transform.position.x < lastPosition.x)
                        {
                            spriteRenderer.flipX = true;   // El lobo se mueve hacia la izquierda
                        }

                        // Guarda la posici�n actual para la pr�xima comparaci�n
                        lastPosition = transform.position;

                        // Actualiza la distancia anterior
                        lastDistance = distanceToPlayer;

                        // Verificar si la distancia entre el lobo y el jugador es menor a la distancia de captura
                        if (Vector3.Distance(transform.position, player.position) < captureDistance)
                        {
                            // Activa el DeadPanel cuando el lobo alcanza al jugador
                            if (deadPanel != null)
                            {
                                deadPanel.SetActive(true);
                            }
                            // Desactiva el lobo
                            DeactivateWolf();
                            isDead = true;
                        }
                    }
                    else
                    {
                        // Si el jugador est� fuera del �rea
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
        // Desactivar completamente al lobo: Desactivar su SpriteRenderer, Animator, y AudioSource
        spriteRenderer.enabled = false;
        animator.enabled = false;
        wolfAudioSource.enabled = false;

        isWolfActive = false;

        // Desactiva "marco" cuando el lobo no est� persiguiendo
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // Limites del mapa (ajusta estos valores según tu mapa)
        float minX = -12f;
        float maxX = 13f;
        float minY = -8f;
        float maxY = 13f;

        // Randomiza el borde en el que aparecerá el lobo
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
        // Cuando el lobo est� listo para aparecer, activar todo lo necesario
        // Activamos el SpriteRenderer, Animator y AudioSource solo cuando el lobo est� por aparecer
        spriteRenderer.enabled = true;
        animator.enabled = true;
        wolfAudioSource.enabled = true;

        // Marca al lobo como activo
        isWolfActive = true;

        // Si es de noche y el lobo debe aparecer, activa el "marco"
        if (marco != null)
        {
            marco.SetActive(true);
        }

        // Reproducir m�sica de lobo
        if (wolfAudioSource != null && !wolfAudioSource.isPlaying)
        {
            wolfAudioSource.Play();
        }
    }
}


