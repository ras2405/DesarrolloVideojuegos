using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyCollection : MonoBehaviour
{
    private bool isPlayerInRange; // Indica si el jugador está en rango.

    public Item lamp;
    public Item lampWithFly;

    private void Update()
    {
        // Si el jugador está en rango y presiona "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem(); // Llama al método para recoger el objeto.
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Comprueba si el objeto que entra es el jugador.
        {
            isPlayerInRange = true; // Indica que el jugador está en rango.
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Indica que el jugador ha salido del rango.
        }
    }

    private void CollectItem()
    {
        if(GameManager.instance.inventoryContainer.selectedItem.name == "Lamp")
        {
            GameManager.instance.inventoryContainer.Remove(lamp);
            GameManager.instance.inventoryContainer.Add(lampWithFly);
            Destroy(gameObject);
        }
    }
}