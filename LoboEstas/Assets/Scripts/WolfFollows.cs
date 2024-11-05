using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 0.75f;
    public float captureDistance = 0.5f; // Distancia mínima para atrapar al jugador

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    private CycleDayController cycleDayController;

    // Referencia al objeto "marco"
    public GameObject marco; // Asigna "marco" desde el Inspector

    // Referencia al DeadPanel
    public GameObject deadPanel; // Asigna el DeadPanel desde el Inspector

    // Referencias de audio
    public AudioSource sceneAudioSource; // música escena
    public AudioSource wolfAudioSource;  // musica lobo

    private bool isWolfActive = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;

        // Busca el controlador del ciclo del día en la escena
        cycleDayController = FindObjectOfType<CycleDayController>();

        // "marco" esté desactivado al iniciar
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // DeadPanel esté desactivado al iniciar
        if (deadPanel != null)
        {
            deadPanel.SetActive(false);
        }

        // el sonido del lobo esté apagado al inicio
        if (wolfAudioSource != null && wolfAudioSource.isPlaying)
        {
            wolfAudioSource.Pause();
        }
    }

    void Update()
    {
        // Verificar si es de noche y si no es después de las 10 PM
        if (cycleDayController != null && cycleDayController.IsNight() && !cycleDayController.IsAfter10PM())
        {
            if (player != null)
            {
                // Verificar si el jugador está dentro del área de -11f y 11f en X e Y
                if (player.position.x >= -11f && player.position.x <= 11f &&
                    player.position.y >= -11f && player.position.y <= 11f)
                {
                    // Hace visible al lobo
                    spriteRenderer.enabled = true;
                    isWolfActive = true;

                    // Activa "marco" cuando el lobo está persiguiendo
                    if (marco != null)
                    {
                        marco.SetActive(true);
                    }

                    // Pausar música de la escena y reproducir sonido del lobo
                    if (sceneAudioSource != null && sceneAudioSource.isPlaying)
                    {
                        sceneAudioSource.Pause();
                    }

                    if (wolfAudioSource != null)
                    {
                        wolfAudioSource.UnPause();
                    }

                    // Calcula la nueva posición hacia el jugador
                    Vector3 targetPosition = player.position + offset;

                    // Usamos MoveTowards para asegurarnos de que el lobo se acerque hasta el jugador
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

                    // Actualiza la dirección del sprite del lobo según su movimiento
                    if (transform.position.x > lastPosition.x)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else if (transform.position.x < lastPosition.x)
                    {
                        spriteRenderer.flipX = true;
                    }

                    lastPosition = transform.position;

                    // Verificar si la distancia entre el lobo y el jugador es menor a la distancia de captura
                    if (Vector3.Distance(transform.position, player.position) < captureDistance)
                    {
                        // Activa el DeadPanel cuando el lobo alcanza al jugador
                        if (deadPanel != null)
                        {
                            deadPanel.SetActive(true);
                        }

                        // Desactiva el lobo
                        gameObject.SetActive(false);
                        isWolfActive = false;

                        if (wolfAudioSource != null)
                        {
                            wolfAudioSource.Pause();
                        }
                    }
                }
                else
                {
                    // si el jugador está fuera del area
                    DisableWolf();
                }
            }
        }
        else
        {
            DisableWolf();
        }
    }

    void DisableWolf()
    {
        // Si es después de las 10 PM o no es de noche, desactiva al lobo
        spriteRenderer.enabled = false;
        isWolfActive = false;

        // Desactiva "marco" cuando el lobo no está persiguiendo
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // Reactivar la música de la escena y detener el sonido del lobo
        if (sceneAudioSource != null)
        {
            sceneAudioSource.UnPause();
        }

        if (wolfAudioSource != null)
        {
            wolfAudioSource.Pause();
        }

        // Mueve al lobo a una posición random (para la próxima aparición)
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);

        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }
}