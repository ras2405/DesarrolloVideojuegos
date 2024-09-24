using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleVegetable : MonoBehaviour
{
    public GameObject onCollectEffect;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] public Inventario inventario;

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.LogError("CollectibleVegetable: ");
        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {
            //Debug.Log("Recolección iniciada: " + cantidadPuntos);
            if (inventario == null)
            {
                Debug.LogError("El inventario no está asignado!");
            }
            else
            {
                //Debug.LogError("El inventario está asignado...");
                inventario.HarvestCarrot(cantidadPuntos);
               // Debug.LogError("Llamamos a collectible display...");
                inventario.UpdateCollectibleDisplay();
            }
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            // Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
    }
}
