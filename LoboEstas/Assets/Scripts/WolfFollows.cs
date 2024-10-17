using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    // la distancia que mantiene con el player
    public Vector3 offset;

    public float followSpeed = 0.75f;

    SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
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
}
