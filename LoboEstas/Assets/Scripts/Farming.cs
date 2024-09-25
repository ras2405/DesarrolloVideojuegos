using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public GameObject carrotPrefab;
    public Item item;
    public int count=1;
    private float state; //0 is growing, 1 is ready to harvest

    //[SerializeField] public Inventario inventario;
    private Inventario inventario;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        if (inventario == null)
        {
            Debug.LogError("No hay una instancia de Inventario en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //   if (other.CompareTag("Carrot")) - Debemos agregar esto para que luego segun el tipo que de tag que tengamos podamos sumarlos
        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {
           // Debug.LogError("Farming: ");
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Calcular una nueva posici�n para la zanahoria
            //Vector3 dropPosition = transform.position + new Vector3(0.10f, 0, 0); // Cambia (1f, 0, 0) por la distancia deseada
                                                                                  //Estaria bueno que luego segun la direccion donde esta el player, dropee para el lado opuesto

            // Deberia dropear una zanahoria aca
            //Instantiate(carrotPrefab, dropPosition, transform.rotation);
            if(GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);
            }
            else{
                Debug.LogWarning("No inventory container attached to the game manager");
            }
            if (inventario == null)
            {
                Debug.LogError("El inventario no est� asignado!");
            }
            else
            {
              //  Debug.LogError("El inventario est� asignado...");
                inventario.HarvestCarrot(1);
              //  Debug.LogError("Llamamos a collectible display...");
                inventario.UpdateCollectibleDisplay();
            }
        }

    }

}