/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public GameObject carrotPrefab;
    public Item item;
    public int count = 1;
    private float state; //0 is growing, 1 is ready to harvest

    private Inventario inventario;

    Animator animator;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        if (inventario == null)
        {
            Debug.LogError("No hay una instancia de Inventario en la escena.");
        }
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Carrot")) - Debemos agregar esto para que luego segun el tipo que de tag que tengamos podamos sumarlos
      
        if (other.GetComponent<PlayerController>() != null)
        {
            animator.SetTrigger("pigCollect");
            Destroy(gameObject);
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Calcular una nueva posicion para la zanahoria
            //Vector3 dropPosition = transform.position + new Vector3(0.10f, 0, 0); // Cambiar (1f, 0, 0) por la distancia deseada
                                                                                    //Estaria bueno que luego segun la direccion donde esta el player, dropee para el lado opuesto
            // Dropear una zanahoria: Instantiate(carrotPrefab, dropPosition, transform.rotation);

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
                inventario.HarvestCarrot(count);
                inventario.UpdateCollectibleDisplay();
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public GameObject carrotPrefab;
    public Item item;
    public int count = 1;
    private float state; //0 is growing, 1 is ready to harvest

    private Inventario inventario;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        if (inventario == null)
        {
            Debug.LogError("No hay una instancia de Inventario en la escena.");
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto colisionado es el jugador
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // Verifica si el nombre del item es "Stone"
            if (item != null && item.name == "Stone")
            {
                // Llama al método en PlayerController para iniciar la animación
                player.StartAnimationCollect();

                // Obtiene la duración de la animación
                float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;

                // Inicia la coroutine para esperar la duración de la animación
                StartCoroutine(WaitForAnimation(player, animationDuration));
            }
            else
            {
                Destroy(gameObject); // Si no es "Stone", destruye inmediatamente
                Instantiate(onCollectEffect, transform.position, transform.rotation);
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
        }
    }

    private IEnumerator WaitForAnimation(PlayerController player, float animationDuration)
    {
        // Espera a que termine la animación
        yield return new WaitForSeconds(animationDuration);

        // Destruye el objeto después de que la animación termine
        Destroy(gameObject);

        Instantiate(onCollectEffect, transform.position, transform.rotation);
    }*/

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && item != null && item.name == "Stone")
        {
            player.StartAnimationCollect(); // Inicia la animación y bloquea el movimiento

            // Obtiene la duración de la animación
            float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;

            // Inicia la coroutine para esperar la duración de la animación
            StartCoroutine(WaitForAnimation(player, animationDuration));
        }
        else
        {
            Destroy(gameObject); // Si no es "Stone", destruye inmediatamente
            Instantiate(onCollectEffect, transform.position, transform.rotation);
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
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && item != null && (
            item.name == "Stone" || item.name == "Wood" || item.name == "Carrot" || item.name == "Firefly"))
        {
            player.StartAnimationCollect(); // Inicia la animación y bloquea el movimiento

            // Obtiene la duración de la animación
            float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;

            // Inicia la coroutine para esperar la duración de la animación
            StartCoroutine(WaitForAnimation(player, animationDuration));
        }
        else
        {
            Destroy(gameObject); // Si no es "Stone", destruye inmediatamente
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }

        // Agregar lógica para el inventario y efectos visuales si es necesario
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
    }

    /*private IEnumerator WaitForAnimation(PlayerController player, float animationDuration)
    {
        Debug.Log("Esperando la duración de la animación: " + animationDuration);
        yield return new WaitForSeconds(animationDuration);
        Debug.Log("Animación completada, destruyendo objeto.");

        Destroy(gameObject);
        Instantiate(onCollectEffect, transform.position, transform.rotation);

        player.UnlockMovement();
        Debug.Log("Movimiento desbloqueado.");
    }*/

    private IEnumerator WaitForAnimation(PlayerController player, float animationDuration)
    {
        // Espera hasta la mitad de la duración de la animación
        yield return new WaitForSeconds(animationDuration / 2);

        // Destruye el objeto y muestra el efecto de recogida
        Destroy(gameObject);
        Instantiate(onCollectEffect, transform.position, transform.rotation);

        // Espera el resto de la duración de la animación
        //yield return new WaitForSeconds(animationDuration / 2);

        // Llama al método para desbloquear el movimiento
        player.UnlockMovement();
    }
}

/*private void OnTriggerEnter2D(Collider2D other)
{
    // if (other.CompareTag("Carrot")) - Debemos agregar esto para que luego segun el tipo que de tag que tengamos podamos sumarlos

    if (other.GetComponent<PlayerController>() != null)
    {
        if (other.CompareTag("Stone"))
        {
            StartAnimationCollect();
        }

        Destroy(gameObject);
        Instantiate(onCollectEffect, transform.position, transform.rotation);

        // Calcular una nueva posicion para la zanahoria
        //Vector3 dropPosition = transform.position + new Vector3(0.10f, 0, 0); // Cambiar (1f, 0, 0) por la distancia deseada
        //Estaria bueno que luego segun la direccion donde esta el player, dropee para el lado opuesto
        // Dropear una zanahoria: Instantiate(carrotPrefab, dropPosition, transform.rotation);

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
            Debug.LogError("El inventario no esta asignado!");
        }
        else
        {
            inventario.HarvestCarrot(count);
            inventario.UpdateCollectibleDisplay();
        }
    }
}
}*/
