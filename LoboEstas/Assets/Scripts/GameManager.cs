using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemContainer inventoryContainer;
    public TileManager tileManager;
    public SaveSystem saveSystem;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        tileManager = GetComponent<TileManager>(); 
    }
}