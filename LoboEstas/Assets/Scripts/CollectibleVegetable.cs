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
        if (other.GetComponent<PlayerController>() != null)
        {
            if (inventario == null)
            {
                Debug.LogError("El inventario no está asignado!");
            }
            else
            {
                inventario.HarvestCarrot(cantidadPuntos);
                inventario.UpdateCollectibleDisplay();
            }
            Destroy(gameObject);
        }
    }
}
