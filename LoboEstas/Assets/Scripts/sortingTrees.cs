using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingTrees : MonoBehaviour
{
    public int startingOrder = 100;

    void Start()
    {
        SortChildren();
    }

    public void SortChildren()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = startingOrder - i;
            }
        }
    }

}
