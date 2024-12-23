using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public Item item;
    public int count = 1;

    public ItemRespawnManager respawnManager; // Referencia al respawn manager
    public ItemRespawnManager.ItemSpawnPoint spawnPoint; // Punto de spawn del ítem

    private Inventario inventario;

    public bool isRespawnable = false;

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
        PlayerController player = other.GetComponent<PlayerController>();
        if(!GameManager.instance.inventoryContainer.IsFull(item))
        {
            if (player.currentSpeed != player.runSpeed)
            {

                if (player != null && item != null && (item.name != "Firefly"))
                 //item.name == "Stone" || item.name == "Wood" || item.name == "Carrot") || item.name == "CarrotSeed")//|| item.name == "Firefly" (DEBEN AGREGARCE AL TARRO)
                {
                    player.StartAnimationCollect(); // Inicia la animación y bloquea el movimiento

                    // Obtiene la duración de la animación
                    float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;

                // Inicia la coroutine para esperar la duración de la animación
                    StartCoroutine(WaitForAnimation(player, animationDuration));
                }
                else
                {
                    Destroy(gameObject); 
                    Instantiate(onCollectEffect, transform.position, transform.rotation);
                }
            }
        }
        
    }

    private IEnumerator WaitForAnimation(PlayerController player, float animationDuration)
    {
        // Espera hasta la mitad de la duración de la animación
        yield return new WaitForSeconds(animationDuration / 2);

        CollectItem();

        // Espera el resto de la duración de la animación
        //yield return new WaitForSeconds(animationDuration / 2);

        // Llama al método para desbloquear el movimiento
        player.UnlockMovement();
    }

    private void CollectItem()
    {
        // Mostrar el efecto visual
        if (onCollectEffect != null)
        {
            GameObject effect = Instantiate(onCollectEffect, transform.position, transform.rotation);
            Destroy(effect, 3f);
        }

        // Agregar el ítem al inventario
        if (GameManager.instance.inventoryContainer != null)
        {
            GameManager.instance.inventoryContainer.Add(item, count);
        }
        else
        {
            Debug.LogWarning("No inventory container attached to the game manager");
        }

        if (inventario == null)
        {
            Debug.LogError("El inventario no está asignado!");
        }
        else
        {
            inventario.HarvestCarrot(count);
            inventario.UpdateCollectibleDisplay();
        }

        // Notificar al respawn manager que el punto de spawn está libre
        if (respawnManager != null && spawnPoint != null)
        {
            respawnManager.OnItemCollected(spawnPoint);
        }

        // Destruir el objeto
        Destroy(gameObject);
    }
}