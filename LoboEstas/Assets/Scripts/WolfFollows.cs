/*using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    // la distancia que mantiene con el player
    public Vector3 offset;

    public float followSpeed = 0.75f;

    SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    private CycleDayController cycleDayController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;

        // busca controlador del ciclo del dia en la escena
        cycleDayController = FindObjectOfType<CycleDayController>();
    }

    void Update()
    {
        if(cycleDayController != null && cycleDayController.IsNight())
        {
            if (player != null)
            {
                // hace visible al lobo
                spriteRenderer.enabled = true;

                // calcula nueva posici�n 
                Vector3 targetPosition = player.position + offset;

                // mueve al lobo
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
            }
        }
        else
        {
            // lobo invisible
            spriteRenderer.enabled = false;

            // mueve al lobo a una posici�n random (para prox aparici�n)
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

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    private CycleDayController cycleDayController;

    // Nueva referencia para el objeto "marco"
    public GameObject marco; // Asigna "marco" desde el Inspector

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;

        // Busca controlador del ciclo del d�a en la escena
        cycleDayController = FindObjectOfType<CycleDayController>();

        // Aseg�rate de que "marco" est� desactivado al iniciar
        if (marco != null)
        {
            marco.SetActive(false);
        }
    }

    void Update()
    {
        if (cycleDayController != null && cycleDayController.IsNight())
        {
            if (player != null)
            {
                // Hace visible al lobo
                spriteRenderer.enabled = true;

                // Activa "marco" cuando el lobo est� persiguiendo
                if (marco != null)
                {
                    marco.SetActive(true);
                }

                // Calcula nueva posici�n 
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
            }
        }
        else
        {
            // Lobo invisible
            spriteRenderer.enabled = false;

            // Desactiva "marco" cuando no persigue
            if (marco != null)
            {
                marco.SetActive(false);
            }

            // Mueve al lobo a una posici�n random (para pr�xima aparici�n)
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(-10f, 10f);

            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
    }
}