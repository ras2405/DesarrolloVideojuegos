using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible2D : MonoBehaviour
{

    public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;
    [SerializeField] private float cantidadPuntos;

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {

            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }


    }


}
