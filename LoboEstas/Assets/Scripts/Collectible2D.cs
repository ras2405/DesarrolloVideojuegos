using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Collectible2D : MonoBehaviour
{

    //public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;

    // Update is called once per frame
    void Update()
    {

       // transform.Rotate(0, 0, rotationSpeed);
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
         // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null) {

            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }

        
    }


}


*/

public class Collectible2D : MonoBehaviour
{

    //public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {

            // Destroy the collectible
            Destroy(gameObject);

            //sumar al inventario
            puntaje.HarvestCarrot(cantidadPuntos);


            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }


    }


}
