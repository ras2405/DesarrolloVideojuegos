using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButtonSprite : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite;   // El sprite normal del objeto
    public Sprite clickedSprite;  // El sprite al hacer clic
    public GameObject targetObject; // El objeto cuyo SpriteRenderer debe cambiar
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (targetObject != null)
        {
            // Obtén el SpriteRenderer del objeto asignado
            spriteRenderer = targetObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                Debug.LogError("El objeto asignado no tiene un SpriteRenderer. Asegúrate de asignar un objeto con un SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogError("No se asignó un objeto objetivo en el Inspector.");
        }
    }

    // Este método se llama cuando el puntero hace clic en el botón (Pointer Down)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = clickedSprite;  // Cambia al sprite de clic
        }
    }

    // Este método se llama cuando el puntero deja de hacer clic en el botón (Pointer Up)
    public void OnPointerUp(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = normalSprite;  // Vuelve al sprite normal
        }
    }
}
