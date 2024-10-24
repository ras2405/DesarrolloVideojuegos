///*using UnityEngine;

//public class FollowPlayer : MonoBehaviour
//{
//    public Transform player;

//    // la distancia que mantiene con el player
//    public Vector3 offset;

//    public float followSpeed = 0.75f;

//    SpriteRenderer spriteRenderer;
//    private Vector3 lastPosition;

//    private CycleDayController cycleDayController;

//    void Start()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        lastPosition = transform.position;

//        // busca controlador del ciclo del dia en la escena
//        cycleDayController = FindObjectOfType<CycleDayController>();
//    }

//    void Update()
//    {
//        if(cycleDayController != null && cycleDayController.IsNight())
//        {
//            if (player != null)
//            {
//                // hace visible al lobo
//                spriteRenderer.enabled = true;

//                // calcula nueva posición 
//                Vector3 targetPosition = player.position + offset;

//                // mueve al lobo
//                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

//                if (transform.position.x > lastPosition.x)
//                {
//                    spriteRenderer.flipX = false;
//                }
//                else if (transform.position.x < lastPosition.x)
//                {
//                    spriteRenderer.flipX = true;
//                }

//                lastPosition = transform.position;
//            }
//        }
//        else
//        {
//            // lobo invisible
//            spriteRenderer.enabled = false;

//            // mueve al lobo a una posición random (para prox aparición)
//            float randomX = Random.Range(-10f, 10f);
//            float randomY = Random.Range(-10f, 10f);

//            transform.position = new Vector3(randomX, randomY, transform.position.z);
//        }
//    }
//}
//*/
///*
//using UnityEngine;

//public class FollowPlayer : MonoBehaviour
//{
//    public Transform player;
//    public Vector3 offset;
//    public float followSpeed = 0.75f;

//    private SpriteRenderer spriteRenderer;
//    private Vector3 lastPosition;

//    private CycleDayController cycleDayController;

//    // Nueva referencia para el objeto "marco"
//    public GameObject marco; // Asigna "marco" desde el Inspector

//    void Start()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        lastPosition = transform.position;

//        // Busca controlador del ciclo del día en la escena
//        cycleDayController = FindObjectOfType<CycleDayController>();

//        // Asegúrate de que "marco" esté desactivado al iniciar
//        if (marco != null)
//        {
//            marco.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        if (cycleDayController != null && cycleDayController.IsNight())
//        {
//            if (player != null)
//            {
//                // Hace visible al lobo
//                spriteRenderer.enabled = true;

//                // Activa "marco" cuando el lobo está persiguiendo
//                if (marco != null)
//                {
//                    marco.SetActive(true);
//                }

//                // Calcula nueva posición 
//                Vector3 targetPosition = player.position + offset;

//                // Mueve al lobo
//                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

//                if (transform.position.x > lastPosition.x)
//                {
//                    spriteRenderer.flipX = false;
//                }
//                else if (transform.position.x < lastPosition.x)
//                {
//                    spriteRenderer.flipX = true;
//                }

//                lastPosition = transform.position;
//            }
//        }
//        else
//        {
//            // Lobo invisible
//            spriteRenderer.enabled = false;

//            // Desactiva "marco" cuando no persigue
//            if (marco != null)
//            {
//                marco.SetActive(false);
//            }

//            // Mueve al lobo a una posición random (para próxima aparición)
//            float randomX = Random.Range(-10f, 10f);
//            float randomY = Random.Range(-10f, 10f);

//            transform.position = new Vector3(randomX, randomY, transform.position.z);
//        }
//    }
//}

//*/
///*
//using UnityEngine;

//public class FollowPlayer : MonoBehaviour
//{
//    public Transform player;
//    public Vector3 offset;
//    public float followSpeed = 0.75f;

//    private SpriteRenderer spriteRenderer;
//    private Vector3 lastPosition;

//    private CycleDayController cycleDayController;

//    // Nueva referencia para el objeto "marco"
//    public GameObject marco; // Asigna "marco" desde el Inspector

//    void Start()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        lastPosition = transform.position;

//        // Busca controlador del ciclo del día en la escena
//        cycleDayController = FindObjectOfType<CycleDayController>();

//        // Asegúrate de que "marco" esté desactivado al iniciar
//        if (marco != null)
//        {
//            marco.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        // Verificar si es noche y si no es después de las 10 PM
//        if (cycleDayController != null && cycleDayController.IsNight() && !cycleDayController.IsAfter10PM())
//        {
//            if (player != null)
//            {
//                // Hace visible al lobo
//                spriteRenderer.enabled = true;

//                // Activa "marco" cuando el lobo está persiguiendo
//                if (marco != null)
//                {
//                    marco.SetActive(true);
//                }

//                // Calcula nueva posición
//                Vector3 targetPosition = player.position + offset;

//                // Mueve al lobo
//                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

//                if (transform.position.x > lastPosition.x)
//                {
//                    spriteRenderer.flipX = false;
//                }
//                else if (transform.position.x < lastPosition.x)
//                {
//                    spriteRenderer.flipX = true;
//                }

//                lastPosition = transform.position;

//                // Si el lobo alcanza al jugador
//                if (Vector3.Distance(transform.position, player.position) < 0.5f)
//                {
//                    // Desactiva el lobo
//                    gameObject.SetActive(false);
//                }
//            }
//        }
//        else
//        {
//            // Si es después de las 10 PM o no es de noche, desactiva al lobo
//            spriteRenderer.enabled = false;

//            // Desactiva "marco" cuando el lobo no está persiguiendo
//            if (marco != null)
//            {
//                marco.SetActive(false);
//            }

//            // Mueve al lobo a una posición random (para próxima aparición)
//            float randomX = Random.Range(-10f, 10f);
//            float randomY = Random.Range(-10f, 10f);

//            transform.position = new Vector3(randomX, randomY, transform.position.z);
//        }
//    }
//}*/

/*
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 0.75f;

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

        // Asegúrate de que "marco" esté desactivado al iniciar
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // Asegúrate de que el DeadPanel esté desactivado al iniciar
        if (deadPanel != null)
        {
            deadPanel.SetActive(false);
        }

        // Asegúrate de que el sonido del lobo esté apagado al inicio
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
                // Hace visible al lobo
                spriteRenderer.enabled = true;
                isWolfActive = true;

                // Activa "marco" cuando el lobo está persiguiendo
                if (marco != null)
                {
                    marco.SetActive(true);
                }

                Debug.Log("antes de modificar audios");
                // Pausar música de la escena y reproducir sonido del lobo
                if (sceneAudioSource != null && sceneAudioSource.isPlaying)
                {
                    sceneAudioSource.Pause();
                    Debug.Log("se hizo stop a sonido ambiente");
                }

                if (wolfAudioSource != null)
                {
                    Debug.Log("se hizo play a wolf audio");
                    wolfAudioSource.UnPause();
                }
                Debug.Log("despeues de modificar audios");

                // Calcula la nueva posición
                Vector3 targetPosition = player.position + offset;

                // Mueve al lobo
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                if (transform.position.x > lastPosition.x)
                {
                    spriteRenderer.flipX = false;
                }
                else if (transform.position.x < lastPosition.x)
                {
                    spriteRenderer.flipX = true;
                }

                lastPosition = transform.position;

                // Si el lobo alcanza al jugador
                if (Vector3.Distance(transform.position, player.position) < 0.5f)
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
        }
        else
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
}




*/

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

        // Asegúrate de que "marco" esté desactivado al iniciar
        if (marco != null)
        {
            marco.SetActive(false);
        }

        // Asegúrate de que el DeadPanel esté desactivado al iniciar
        if (deadPanel != null)
        {
            deadPanel.SetActive(false);
        }

        // Asegúrate de que el sonido del lobo esté apagado al inicio
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
        }
        else
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
}