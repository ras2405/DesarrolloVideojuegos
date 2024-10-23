using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingTrees : MonoBehaviour
{
    public int startingOrder = 100;

    void Start()
    {
        // Llama a la funci�n para ordenar al iniciar el juego
        SortChildren();
    }

    public void SortChildren()
    {
        // Obt�n todos los hijos del objeto padre
        /* for (int i = 0; i < transform.childCount; i++)
         {
             Transform child = transform.GetChild(i);
             SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

             if (spriteRenderer != null)
             {
                 // Asigna el sortingOrder basado en la posici�n en la jerarqu�a
                 // Los objetos que est�n m�s arriba en la jerarqu�a tendr�n un sortingOrder mayor
                 spriteRenderer.sortingOrder = -(i + 1);
             }
         }*/


        // Obt�n todos los hijos del objeto padre
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
