using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PurchaseItem : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] Item item;
    // Este método se llamará cuando se detecte un clic sobre el objeto
    public void OnPointerClick(PointerEventData eventData)
    {
        // Verificar si el clic fue el derecho
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Llamar a la función que quieres ejecutar
            Debug.LogWarning("Click derecho presionado");
            OnRightClick();
        }
    }

    // Función que se ejecutará cuando se detecte el clic derecho
    private void OnRightClick()
    {
        int count =1;
        GameManager.instance.inventoryContainer.Add(item,count);
    }
}
