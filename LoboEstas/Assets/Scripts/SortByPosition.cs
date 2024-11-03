using UnityEngine;

public class SortByPosition : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Accede al componente SpriteRenderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Establece el sortingOrder al inicio
        UpdateSortingOrder();
    }

    void Update()
    {
        // Actualiza el sortingOrder cada frame
        UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        // Cambia el sortingOrder basado en la coordenada Y del objeto
        // Multiplicamos por -10 para que los objetos más altos aparezcan detrás
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10);
    }
}