using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingTrees : MonoBehaviour
{
    public int startingOrder = 100;

    void Start()
    {
        // Llama a la función para ordenar al iniciar el juego
        SortChildren();
    }

    public void SortChildren()
    {
        // Obtén todos los hijos del objeto padre
        /* for (int i = 0; i < transform.childCount; i++)
         {
             Transform child = transform.GetChild(i);
             SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

             if (spriteRenderer != null)
             {
                 // Asigna el sortingOrder basado en la posición en la jerarquía
                 // Los objetos que están más arriba en la jerarquía tendrán un sortingOrder mayor
                 spriteRenderer.sortingOrder = -(i + 1);
             }
         }*/


        // Obtén todos los hijos del objeto padre
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Asigna el sortingOrder comenzando desde 'startingOrder' y disminuyendo
                spriteRenderer.sortingOrder = startingOrder - i;
            }
        }
    }

}
