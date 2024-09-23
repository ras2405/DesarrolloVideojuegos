using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleVegetable : MonoBehaviour
{
    public GameObject onCollectEffect;
    public Item item;
    public int count =1;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] public Inventario inventario;


    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Recolecci�n iniciada: " + cantidadPuntos);
            if (inventario == null)
            {
                Debug.LogError("El inventario no est� asignado!");
            }
            else
            {
                inventario.HarvestCarrot(cantidadPuntos);
            }
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            // Instantiate(onCollectEffect, transform.position, transform.rotation);
        }

        if(GameManager.instance.inventoryContainer != null)
        {
            GameManager.instance.inventoryContainer.Add(item,count);
        }
        else{
            Debug.LogError("No inventory container attached to game manager");
        }
    }
}
