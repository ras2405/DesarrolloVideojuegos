using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButtonSprite : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite;   
    public Sprite clickedSprite;  
    public GameObject targetObject; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (targetObject != null)
        {
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = clickedSprite;  
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = normalSprite; 
        }
    }
}
