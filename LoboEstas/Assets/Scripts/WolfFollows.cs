using UnityEngine;

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

                // calcula nueva posición 
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

            // mueve al lobo a una posición random (para prox aparición)
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(-10f, 10f);

            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
    }
}
