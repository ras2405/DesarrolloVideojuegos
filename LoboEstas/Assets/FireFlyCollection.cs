using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyCollection : MonoBehaviour
{
    private bool isPlayerInRange; 

    public Item lamp;
    public Item lampWithFly;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; 
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