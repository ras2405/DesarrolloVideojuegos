using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject onCollectEffect;
    public GameObject carrotPrefab;
    private float state; //0 is growing, 1 is ready to harvest

    // Start is called before the first frame update
    //void Start()
   // {

  //  }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {

            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Calcular una nueva posición para la zanahoria
            Vector3 dropPosition = transform.position + new Vector3(0.10f, 0, 0); // Cambia (1f, 0, 0) por la distancia deseada
            //Estaria bueno que luego segun la direccion donde esta el player, dropee para el lado opuesto

            // Deberia dropear una zanahoria aca
            Instantiate(carrotPrefab, dropPosition, transform.rotation);
        }


    }

}