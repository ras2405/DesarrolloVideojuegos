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

        if (other.GetComponent<PlayerController>() != null)
        {
            Destroy(gameObject);

            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
    }
}
