using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 1.5f;  // Velocidad normal del lobo
    public float captureDistance = 0.5f; // Distancia m�nima para atrapar al jugador
    public float transitionDistance = 2.0f; // Distancia para cambiar entre caminata1 y caminata2
    public float reducedSpeed = 0.4f; // Velocidad reducida cuando el lobo est� cerca del jugador

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource wolfAudioSource;

    private CycleDayController cycleDayController;

    // Referencia al objeto "marco"
    public GameObject marco; // Asigna "marco" desde el Inspector

    // Referencia al DeadPanel
    public GameObject deadPanel; // Asigna el DeadPanel desde el Inspector

    private bool isWolfActive = false;
    private bool isWolfReady = false; // Nuevo indicador para saber si el lobo est� listo para aparecer
    private Vector3 lastPosition;

    // Variables para la histeresis
    private float lastDistance = Mathf.Infinity; // Distancia anterior para comparar

    // Agregamos las referencias para los sonidos de la rama y el aullido
    public AudioSource branchBreakSound;  // Sonido de rama quebr�ndose
                                          // Sonido de aullido

    // Para controlar el momento de la activaci�n de la rama
    private bool hasBranchSoundPlayed = false;

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
        // Verificar si es de noche y si no es despu�s de las 10 PM
        if (cycleDayController != null && cycleDayController.IsNight())
        {
            // Si el lobo no est� activo y es de noche, activarlo
            if (!isWolfActive && !isWolfReady)
            {
                // Hacer que el lobo est� listo para aparecer
                isWolfReady = true;

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
                if (player.position.x >= -11f && player.position.x <= 11f &&
                    player.position.y >= -11f && player.position.y <= 11f)
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
                    }
                }
                else
                {
                    // Si el jugador est� fuera del �rea
                    DeactivateWolf();
                    isWolfActive = true;
                }
            }
        }
        else
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

        // Mueve al lobo a una posici�n random (para la pr�xima aparici�n)
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);

        transform.position = new Vector3(randomX, randomY, transform.position.z);
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


