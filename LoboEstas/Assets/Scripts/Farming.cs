using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public Item item;
    public int count = 1;

    public ItemRespawnManager respawnManager; 
    public ItemRespawnManager.ItemSpawnPoint spawnPoint; 

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
                {
                    player.StartAnimationCollect(); 
                    float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;
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
        yield return new WaitForSeconds(animationDuration / 2);

        CollectItem();
        player.UnlockMovement();
    }

    private void CollectItem()
    {
        if (onCollectEffect != null)
        {
            GameObject effect = Instantiate(onCollectEffect, transform.position, transform.rotation);
            Destroy(effect, 3f);
        }

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

        if (respawnManager != null && spawnPoint != null)
        {
            respawnManager.OnItemCollected(spawnPoint);
        }

        Destroy(gameObject);
    }
}